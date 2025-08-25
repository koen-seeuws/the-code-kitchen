using FluentValidation;
using TheCodeKitchen.Application.Validation;
using TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Validation;

public class CreateRecipeFormModelValidator : ValidatorBase<CreateRecipeFormModel>
{
    public CreateRecipeFormModelValidator()
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