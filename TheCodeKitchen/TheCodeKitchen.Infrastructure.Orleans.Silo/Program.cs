using Azure.Data.Tables;
using Orleans.Configuration;
using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Application.Business.Interceptors;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Infrastructure.Extensions;
using TheCodeKitchen.Infrastructure.Logging.Serilog;
using TheCodeKitchen.Infrastructure.Orleans;

var builder = Host.CreateApplicationBuilder(args);


// Application Services
builder.Services.AddApplicationServices();

// Infrastructure Services
builder.Logging.RegisterSerilog();

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

#if DEBUG
// TODO: REMOVE, this is only for development purposes to ensure a clean state.
//foreach (var storage in TheCodeKitchenState.All) { tableClient.DeleteTable(storage); }
#endif

builder.Services.AddSignalRManagementServices(builder.Configuration);

builder.UseOrleans(silo =>
{
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = siloConfiguration.ClusterId;
        options.ServiceId = siloConfiguration.ServiceId;
    });

    silo.AddIncomingGrainCallFilter<LoggingInterceptor>();

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

    silo.UseDashboard();
});

var host = builder.Build();
host.Run();