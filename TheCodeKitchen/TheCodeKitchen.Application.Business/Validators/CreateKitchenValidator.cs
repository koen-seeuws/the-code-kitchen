using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Validators;

public class CreateKitchenValidator : AbstractValidator<CreateKitchenRequest>
{
    public CreateKitchenValidator()
    {
        RuleFor(k => k.Name).NotEmpty();
        RuleFor(k => k.GameId).NotEmpty();
    }
}