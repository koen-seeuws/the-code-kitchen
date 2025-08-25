using FluentValidation;
using TheCodeKitchen.Application.Validation;
using TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Validation;

public class CreateIngredientFormModelValidator : ValidatorBase<CreateIngredientFormModel>
{
    public CreateIngredientFormModelValidator()
    {
        RuleFor(i => i.Name).NotEmpty().MaximumLength(100);
    }
}