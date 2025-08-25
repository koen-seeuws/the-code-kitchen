using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Validators;

public class ConfirmMessageValidator : AbstractValidator<ConfirmMessageRequest>
{
    public ConfirmMessageValidator()
    {
        RuleFor(c => c.Number).NotEmpty().GreaterThan(0);
    }
}