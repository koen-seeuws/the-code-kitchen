using Azure.Data.Tables;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using Orleans.Configuration;
using OrleansDashboard.Implementation.Details;
using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Application.Contracts.Contants;
using TheCodeKitchen.Infrastructure.Extensions;
using TheCodeKitchen.Infrastructure.Orleans;

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
    
    silo.UseAzureStorageClustering(options => { options.TableServiceClient = tableClient; });

    silo.AddAzureTableGrainStorageAsDefault(options => { options.TableServiceClient = tableClient; });

    silo.UseAzureTableReminderService(options => { options.TableServiceClient = tableClient; });

    silo.AddStreaming()
        .AddAzureQueueStreams(TheCodeKitchenStreams.Default,
            options =>
            {
                options.Configure(azureQueueOptions => { azureQueueOptions.QueueServiceClient = queueClient; });
            });
});

var host = builder.Build();
host.Run();