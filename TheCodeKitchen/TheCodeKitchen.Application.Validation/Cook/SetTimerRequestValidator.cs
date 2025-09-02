using FluentValidation;
using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Validation.Cook;

public sealed class SetTimerValidator : AbstractValidator<SetTimerRequest>
{
    public SetTimerValidator()
    {
        RuleFor(x => x.Time).NotEmpty().GreaterThan(TimeSpan.Zero);
    }
}