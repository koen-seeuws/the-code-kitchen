using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheCodeKitchen.Server.Application.Business.Pipeline;
using TheCodeKitchen.Server.Core.Shared;

namespace TheCodeKitchen.Server.Application.Business.Configuration;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, string theCodeKitchenOptionsSection = "TheCodeKitchen")
    {
        //FluentValidation
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServiceRegistration));

        //MediatR
        services.AddMediatR(mediatr =>
        {
            //Handlers
            mediatr.RegisterServicesFromAssemblyContaining(typeof(ApplicationServiceRegistration));

            //Pipeline
            mediatr
                .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
                .AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>))
                .AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        //Options
        var theCodeKitchenOptions =
            configuration.BindAndValidateConfiguration<TheCodeKitchenServerOptions, TheCodeKitchenServerOptionsValidator>(
                theCodeKitchenOptionsSection);

        services.AddSingleton(Options.Create(theCodeKitchenOptions));
        
        return services;
    }
}