using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Validators;

public class StopTimerValidator : AbstractValidator<StopTimerRequest>
{
    public StopTimerValidator()
    {
        RuleFor(s => s.Number).NotEmpty().GreaterThan(0);
    }
}