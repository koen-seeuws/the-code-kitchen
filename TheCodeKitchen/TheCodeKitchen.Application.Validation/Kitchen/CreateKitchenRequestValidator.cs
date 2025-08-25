using FluentValidation;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Validation.Kitchen;

public class CreateKitchenValidator : AbstractValidator<CreateKitchenRequest>
{
    public CreateKitchenValidator()
    {
        RuleFor(k => k.Name).NotEmpty().MaximumLength(50);
    }
}