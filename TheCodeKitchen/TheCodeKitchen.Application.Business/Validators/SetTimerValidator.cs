using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Validators;

public class SetTimerRequestValidator : AbstractValidator<SetTimerRequest>
{
    public SetTimerRequestValidator()
    {
        RuleFor(x => x.Time).NotEmpty().GreaterThan(TimeSpan.Zero);
    }
}