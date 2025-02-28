using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheCodeKitchen.Server.Infrastructure.AzureServiceBus;

public static class AzureServiceBusServiceRegistration
{
    public static IServiceCollection AddAzureServiceBusServices(this IServiceCollection services, IConfiguration configuration, string connectionStringKey = "AzureServiceBus")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"The ConnectionString {connectionStringKey} cannot be empty for AzureServiceBus services");
        
        services.AddScoped(_ => new ServiceBusClient(connectionString));
        
        return services;
    }
}