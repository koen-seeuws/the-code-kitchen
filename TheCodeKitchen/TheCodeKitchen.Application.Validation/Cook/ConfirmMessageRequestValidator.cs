using FluentValidation;
using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Validation.Cook;

public class ConfirmMessageValidator : AbstractValidator<ConfirmMessageRequest>
{
    public ConfirmMessageValidator()
    {
        RuleFor(c => c.Number).NotEmpty().GreaterThan(0);
    }
}