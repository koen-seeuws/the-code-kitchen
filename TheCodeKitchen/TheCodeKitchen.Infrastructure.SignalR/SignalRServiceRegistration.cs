using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Infrastructure.SignalR.Services;

namespace TheCodeKitchen.Infrastructure.SignalR;

public static class SignalRServiceRegistration
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddSingleton<IRealtimeGameManagementService, GameManagementSignalRService>();
        
        return services;
    }
}