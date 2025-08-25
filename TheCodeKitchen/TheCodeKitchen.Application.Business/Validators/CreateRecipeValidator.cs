using TheCodeKitchen.Application.Contracts.Requests.CookBook;

namespace TheCodeKitchen.Application.Business.Validators;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeRequest>
{
    public CreateRecipeValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.Ingredients)
            .NotEmpty()
            .Must(i => i.Count >= 2)
            .WithMessage("At least two ingredients are required.")
            .Must(i => i.Count != i.DistinctBy(i => i.Name, StringComparer.OrdinalIgnoreCase).Count())
            .WithMessage("Duplicate ingredients are not allowed.");
    }
}