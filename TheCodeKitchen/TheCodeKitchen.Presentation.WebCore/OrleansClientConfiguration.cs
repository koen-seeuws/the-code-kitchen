using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheCodeKitchen.Application.Contracts.Contants;

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
                
                client.AddStreaming()
                    .AddMemoryStreams(TheCodeKitchenStreams.Default);
            }
        });
    }
}