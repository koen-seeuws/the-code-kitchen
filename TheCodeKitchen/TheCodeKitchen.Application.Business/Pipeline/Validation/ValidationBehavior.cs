using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace TheCodeKitchen.Application.Business.Pipeline.Validation;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
    where TResponse : notnull
{
    public async Task<Result<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken
    )
    {
        if (!validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationTasks = validators.Select(validator => validator.ValidateAsync(context, cancellationToken = default));

        var validationResults = await Task.WhenAll(validationTasks);

        var failures = validationResults
            .Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            return new Result<TResponse>(new ValidationException(failures));
        }

        return await next();
    }
}