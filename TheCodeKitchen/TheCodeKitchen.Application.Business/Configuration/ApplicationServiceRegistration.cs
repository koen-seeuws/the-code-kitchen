using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Business.Pipeline;
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
            mediatr
                .AddOpenBehavior(typeof(ValidationBehaviourWithResultSupport<,>))
                .AddOpenBehavior(typeof(ValidationBehaviourWithoutResultSupport<,>));
        });
        
        return services;
    }
}