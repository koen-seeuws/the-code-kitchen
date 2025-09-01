using FluentValidation;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Models.Recipe;

namespace TheCodeKitchen.Application.Validation.CookBook;

public class RecipeStepDtoValidator : AbstractValidator<RecipeStepDto>
{
    public RecipeStepDtoValidator()
    {
        RuleFor(s => s.EquipmentType).IsInCollection(EquipmentType.Steppable);;
        RuleFor(s => s.Time).GreaterThan(TimeSpan.Zero);
    }
}