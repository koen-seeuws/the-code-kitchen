using Azure.Data.Tables;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using TheCodeKitchen.Application.Contracts.Contants;
using TheCodeKitchen.Infrastructure.Extensions;

namespace TheCodeKitchen.Presentation;

public static class OrleansClientRegistration
{
    public static void AddTheCodeKitchenOrleansClient(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        var clientConfiguration =
            configuration
                .BindAndValidateConfiguration<OrleansClientConfiguration, OrleansClientConfigurationValidator>(
                    "Orleans");

        var azureStorageConnectionString =
            configuration.GetConnectionString("AzureStorage") ??
            throw new InvalidOperationException("ConnectionStrings__AzureStorage is not configured.");

        var eventHubConnectionString =
            configuration.GetConnectionString("EventHub") ??
            throw new InvalidOperationException("ConnectionStrings__EventHubNamespace is not configured.");

        var tableClient = new TableServiceClient(azureStorageConnectionString);
        var queueClient = new QueueServiceClient(azureStorageConnectionString);

        services.AddOrleansClient(client =>
        {
            client.Configure<ClusterOptions>(options =>
            {
                options.ClusterId = clientConfiguration.ClusterId;
                options.ServiceId = clientConfiguration.ServiceId;
            });

            client.UseAzureStorageClustering(options =>
            {
                options.TableServiceClient = tableClient;
                options.TableName = "TheCodeKitchenClustering";
            });

            client
                .AddStreaming()
                .AddEventHubStreams(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider,
                    eventHubConfigurator =>
                    {
                        eventHubConfigurator.ConfigureEventHub(eventHubBuilder =>
                        {
                            eventHubBuilder.Configure(options =>
                            {
                                options.ConfigureEventHubConnection(
                                    eventHubConnectionString,
                                    clientConfiguration.Streaming?.EventHub,
                                    clientConfiguration.Streaming?.ConsumerGroup
                                );
                            });
                        });
                    });

            /*
            .AddAzureQueueStreams(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider,
                options =>
                {
                    options.Configure(azureQueueOptions =>
                    {
                        azureQueueOptions.QueueServiceClient = queueClient;
                        azureQueueOptions.QueueNames = TheCodeKitchenStreams.AzureStorageQueues;
                    });
                });
                */
        });
    }
}