using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Infrastructure.Common.Services;

namespace TheCodeKitchen.Infrastructure.Common;

public static class CommonInfrastructureServiceRegistration
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICodeGenerator, CodeGenerator>();
        
        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        
        return services;
    }
}