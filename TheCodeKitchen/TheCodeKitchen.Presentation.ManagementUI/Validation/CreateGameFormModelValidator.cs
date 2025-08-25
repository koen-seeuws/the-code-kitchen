using FluentValidation;
using TheCodeKitchen.Application.Validation;
using TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Validation;

public class CreateGameFormModelValidator : ValidatorBase<CreateGameFormModel>
{
    public CreateGameFormModelValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(50);
        RuleFor(g => g.SpeedModifier).InclusiveBetween(0.1, 10);
        RuleFor(g => g.MinimumItemsPerOrder)
            .InclusiveBetween((short)1, (short)250)
            .LessThanOrEqualTo(g => g.MaximumItemsPerOrder);
        RuleFor(g => g.MaximumItemsPerOrder)
            .InclusiveBetween((short)1, (short)250)
            .GreaterThanOrEqualTo(g => g.MinimumItemsPerOrder);
        RuleFor(g => g.OrderSpeedModifier).InclusiveBetween(0.1, 10);
    }
}