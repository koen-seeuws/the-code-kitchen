using FluentValidation;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;

namespace TheCodeKitchen.Application.Validation.Pantry;

public class TakeFoodFromPantryValidator : AbstractValidator<TakeFoodFromPantryRequest>
{
    public TakeFoodFromPantryValidator()
    {
        RuleFor(x => x.Ingredient).NotEmpty().MaximumLength(100);
    }
}