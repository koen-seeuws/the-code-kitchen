using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Contracts.Interfaces.Queueing;

namespace TheCodeKitchen.Infrastructure.AzureServiceBus;

public static class AzureServiceBusServiceRegistration
{
    public static IServiceCollection AddAzureServiceBusServices(this IServiceCollection services, IConfiguration configuration, string connectionStringKey = "AzureServiceBus")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"The ConnectionString {connectionStringKey} cannot be empty for AzureServiceBus services");
        
        services.AddSingleton(_ => new ServiceBusClient(connectionString));
        services.AddSingleton<IMessagePublisher, AzureServiceBusMessagePublisher>();
        
        return services;
    }
}