using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheCodeKitchen.Presentation.WebCore;

public static class OrleansClientConfiguration
{
    public static void AddTheCodeKitchenOrleansClient(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddOrleansClient(client =>
        {
            if (environment.IsDevelopment())
            {
                client.UseLocalhostClustering();
            }
        });
    }
}