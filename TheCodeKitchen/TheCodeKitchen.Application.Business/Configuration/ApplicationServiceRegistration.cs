using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Business.Pipeline.Validation;

namespace TheCodeKitchen.Application.Business.Configuration;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //FluentValidation
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServiceRegistration));

        //AutoMapper
        services.AddAutoMapper(typeof(ApplicationServiceRegistration));

        //MediatR
        services.AddMediatR(mediatr =>
        {
            //Handlers
            mediatr.RegisterServicesFromAssemblyContaining(typeof(ApplicationServiceRegistration));

            //Pipeline
            mediatr.AddBehaviorsWithResultFromAssemblyContaining<CreateGameCommand>(typeof(ValidationBehavior<,>));
        });

        return services;
    }
    
    
}