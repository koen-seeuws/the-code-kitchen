using FluentValidation;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Validation;
using TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Validation;

public sealed class StepFormModelValidator : AbstractValidator<StepFormModel>
{
    public StepFormModelValidator()
    {
        RuleFor(s => s.EquipmentType).IsInCollection(EquipmentType.Steppable);
        RuleFor(s => s.Time)
            .NotNull()
            .GreaterThan(TimeSpan.Zero);
    }
}