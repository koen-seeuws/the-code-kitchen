using TheCodeKitchen.Application.Contracts.Requests.Pantry;

namespace TheCodeKitchen.Application.Business.Validators;

public class CreateIngredientValidator : AbstractValidator<CreateIngredientRequest>
{
    public CreateIngredientValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
    }
}