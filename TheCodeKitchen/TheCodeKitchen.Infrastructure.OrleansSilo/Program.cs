using Azure.Data.Tables;
using Azure.Storage.Queues;
using Orleans.Configuration;
using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Application.Contracts.Contants;
using TheCodeKitchen.Infrastructure.Extensions;
using TheCodeKitchen.Infrastructure.OrleansSilo;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

var siloConfiguration =
    builder.Configuration
        .BindAndValidateConfiguration<OrleansSiloConfiguration, OrleansSiloConfigurationValidator>("Orleans");

var azureStorageConnectionString =
    builder.Configuration.GetConnectionString("AzureStorage") ??
    throw new InvalidOperationException("ConnectionStrings__AzureStorage is not configured.");

var tableClient = new TableServiceClient(azureStorageConnectionString);
var queueClient = new QueueServiceClient(azureStorageConnectionString);

builder.UseOrleans(silo =>
{
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = siloConfiguration.ClusterId;
        options.ServiceId = siloConfiguration.ServiceId;
    });

    silo.UseAzureStorageClustering(options =>
    {
        options.TableServiceClient = tableClient;
        options.TableName = "TheCodeKitchenClustering";
    });

    foreach (var storage in TheCodeKitchenState.All)
    {
        silo.AddAzureTableGrainStorage(storage, options =>
        {
            options.TableServiceClient = tableClient;
            options.TableName = storage;
        });
    }

    silo.UseAzureTableReminderService(options =>
    {
        options.TableServiceClient = tableClient;
        options.TableName = "TheCodeKitchenReminders";
    });

    silo
        .AddStreaming()
        .AddAzureQueueStreams(TheCodeKitchenStreams.AzureStorageQueuesProvider,
            options =>
            {
                options.Configure(azureQueueOptions =>
                {
                    azureQueueOptions.QueueServiceClient = queueClient;
                    azureQueueOptions.QueueNames = TheCodeKitchenStreams.AzureStorageQueues;
                });
            });

    silo.UseDashboard();
});

var host = builder.Build();
host.Run();