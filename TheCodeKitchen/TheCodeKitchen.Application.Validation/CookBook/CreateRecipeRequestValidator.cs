using FluentValidation;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;

namespace TheCodeKitchen.Application.Validation.CookBook;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeRequest>
{
    public CreateRecipeValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Ingredients)
            .NotEmpty()
            .Must(i => i.Count >= 2)
            .WithMessage("At least two ingredients are required.")
            .Must(i => i.Count != i.DistinctBy(ri => ri.Name, StringComparer.OrdinalIgnoreCase).Count())
            .WithMessage("Duplicate ingredients are not allowed.");
    }
}