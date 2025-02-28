using FluentValidation;
using MediatR;
using TheCodeKitchen.Server.Application.Contracts.Responses;
using TheCodeKitchen.Server.Core.Enums;

namespace TheCodeKitchen.Server.Application.Business.Pipeline;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        if (!validators.Any()) return await next();

        var failureMessages = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => new Message(failure.ErrorMessage, Reason.NotValid, failure.PropertyName))
            .Distinct()
            .ToList();

        return failureMessages.Count == 0
            ? await next()
            : new TResponse { Succeeded = false, Messages = failureMessages };
    }
}