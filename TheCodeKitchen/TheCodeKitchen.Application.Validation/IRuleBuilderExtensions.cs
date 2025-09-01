using FluentValidation;

namespace TheCodeKitchen.Application.Validation;

public static class IRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> IsInCollection<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        IEnumerable<TProperty> validValues,
        string? errorMessage = null)
    {
        return ruleBuilder
            .Must(validValues.Contains)
            .WithMessage(errorMessage ?? "{PropertyName} must be one of the allowed values.");
    }
}