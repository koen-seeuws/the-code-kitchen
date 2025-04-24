using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Extensions;

public static class MediatrExtensions
{
    private static readonly Type RequestOpenGenericType = typeof(IRequest<>);
    private static readonly Type ResultOpenGenericType = typeof(Result<>);

    // https://github.com/jbogard/MediatR/issues/626
    public static MediatRServiceConfiguration AddBehaviorsWithResultFromAssemblyContaining<TScanAssembly>(
        this MediatRServiceConfiguration configuration, Type openBehaviorType)
    {
        var requestsAssembly = typeof(TScanAssembly).Assembly;

        // Get all types implementing IRequest<> (CreateGameRequest, AddKitchenRequest, etc.)
        var openRequestTypes = GetTypesImplementingOpenGenericType(RequestOpenGenericType, requestsAssembly).ToList();

        foreach (var openRequestType in openRequestTypes)
        {
            // IRequest<Result<CreateGameResponse>>
            var concreteRequestType = openRequestType
                .GetInterfaces()
                .Single(interfaceType => interfaceType.IsGenericType &&
                                         RequestOpenGenericType.IsAssignableFrom(
                                             interfaceType.GetGenericTypeDefinition()));

            // Result<CreateGameResponse>
            var resultType = concreteRequestType
                .GetGenericArguments()
                .SingleOrDefault(interfaceType =>
                    interfaceType.IsGenericType &&
                    ResultOpenGenericType.IsAssignableFrom(interfaceType.GetGenericTypeDefinition())
                );

            // Skip IRequest<T> where T is not a Result<>
            if (resultType == null) continue;

            // CreateGameResponse (LanguageExt Result always has a single generic argument)
            var responseType = resultType.GetGenericArguments().Single();

            // IPipelineBehavior<CreateGameCommand, Result<CreateGameResponse>>
            var behaviorType =
                typeof(IPipelineBehavior<,>).MakeGenericType(openRequestType, resultType);

            // ValidationBehavior<CreateGameCommand, CreateGameResponse>
            var implementationType =
                openBehaviorType.MakeGenericType(openRequestType,
                    responseType);

            configuration.AddBehavior(behaviorType, implementationType);
        }

        return configuration;
    }

    private static IEnumerable<Type> GetTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly) =>
        from type in assembly.GetTypes()
        from interfaceType in type.GetInterfaces()
        let baseType = type.BaseType
        where
            (baseType is { IsGenericType: true } &&
             openGenericType.IsAssignableFrom(baseType.GetGenericTypeDefinition()))
            ||
            (interfaceType.IsGenericType && openGenericType.IsAssignableFrom(interfaceType.GetGenericTypeDefinition()))
        select type;
}