using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Business.Pipeline.Validation;
using TheCodeKitchen.Application.Business.Services;
using TheCodeKitchen.Application.Contracts.Interfaces.Events;

namespace TheCodeKitchen.Application.Business;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        
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