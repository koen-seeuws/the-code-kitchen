using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheCodeKitchen.Infrastructure.Extensions;

namespace TheCodeKitchen.Infrastructure.AzureSignalR;

public static class SignalRServiceRegistration
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment, string azureSignalRSection = "AzureSignalR")
    {
        var signalRServerBuilder = services.AddSignalR();

        if (environment.IsDevelopment()) return services;

        var azureSignalRConfiguration = configuration.BindAndValidateConfiguration<
            AzureSignalRConfiguration,
            AzureSignalRConfigurationValidator
        >(azureSignalRSection);

        signalRServerBuilder.AddAzureSignalR(options =>
        {
            options.ConnectionString = azureSignalRConfiguration.ConnectionString;
            options.ApplicationName = azureSignalRConfiguration.ApplicationName;
        });

        return services;
    }
}