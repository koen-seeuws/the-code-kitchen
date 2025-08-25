using Azure.Data.Tables;
using Orleans.Configuration;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Infrastructure.Extensions;
using TheCodeKitchen.Infrastructure.Orleans;

var builder = Host.CreateApplicationBuilder(args);

var siloConfiguration =
    builder.Configuration
        .BindAndValidateConfiguration<OrleansConfiguration, OrleansConfigurationValidator>(
            "TheCodeKitchenOrleans");

var azureStorageConnectionString =
    builder.Configuration.GetConnectionString("AzureStorage") ??
    throw new InvalidOperationException("ConnectionStrings__AzureStorage is not configured.");

var eventHubConnectionString =
    builder.Configuration.GetConnectionString("EventHubNamespace") ??
    throw new InvalidOperationException("ConnectionStrings__EventHubNamespace is not configured.");

var tableClient = new TableServiceClient(azureStorageConnectionString);

builder.UseOrleans(silo =>
{
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = siloConfiguration.ClusterId;
        options.ServiceId = siloConfiguration.ServiceId;
    });

    if (builder.Environment.IsDevelopment())
        silo.ConfigureEndpoints(11112, 30001);

    silo.UseAzureStorageClustering(options =>
    {
        options.TableServiceClient = tableClient;
        options.TableName = TheCodeKitchenState.Clustering;
    });

    foreach (var storage in TheCodeKitchenState.All)
    {
        silo.AddAzureTableGrainStorage(storage, options =>
        {
            options.TableServiceClient = tableClient;
            options.TableName = storage;
            options.UseStringFormat = true;
        });
    }

    silo.UseAzureTableReminderService(options =>
    {
        options.TableServiceClient = tableClient;
        options.TableName = TheCodeKitchenState.Reminders;
    });

    silo
        .AddStreaming()
        .AddEventHubStreams(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider, eventHubConfigurator =>
        {
            eventHubConfigurator.ConfigureEventHub(eventHubBuilder =>
            {
                eventHubBuilder.Configure(options =>
                {
                    options.ConfigureEventHubConnection(
                        eventHubConnectionString,
                        siloConfiguration.Streaming?.EventHub,
                        siloConfiguration.Streaming?.ConsumerGroup
                    );
                });
            });

            eventHubConfigurator.UseAzureTableCheckpointer(azureTableBuilder =>
                azureTableBuilder.Configure(options =>
                {
                    options.TableServiceClient = tableClient;
                    options.TableName = TheCodeKitchenState.EventHubCheckpoints;
                })
            );
        });

    silo.UseDashboard(options => options.HostSelf = true);
});

var host = builder.Build();
host.Run();