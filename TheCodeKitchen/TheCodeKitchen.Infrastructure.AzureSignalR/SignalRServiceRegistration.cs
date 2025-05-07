using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheCodeKitchen.Infrastructure.AzureSignalR;

public static class SignalRServiceRegistration
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        var signalRServerBuilder = services.AddSignalR();

        if (!environment.IsDevelopment())
        {
            var connectionString = configuration.GetConnectionString("AzureSignalR");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Azure SignalR connection string is not configured.");
            }

            signalRServerBuilder.AddAzureSignalR(options => options.ConnectionString = connectionString);
        }


        return services;
    }
}