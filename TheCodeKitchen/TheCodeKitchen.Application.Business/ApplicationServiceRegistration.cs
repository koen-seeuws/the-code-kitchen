using Microsoft.Extensions.DependencyInjection;

namespace TheCodeKitchen.Application.Business;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //FluentValidation
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServiceRegistration));
        
        //AutoMapper
        services.AddAutoMapper(typeof(ApplicationServiceRegistration));

        return services;
    }
    
    
}