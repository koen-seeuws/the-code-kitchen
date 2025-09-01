using FluentValidation;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Requests.Game;

namespace TheCodeKitchen.Application.Validation.Game;

public class CreateGameValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(50);
        RuleFor(g => g.TimePerMoment).InclusiveBetween(
            TheCodeKitchenTimePerMoment.Minimum,
            TheCodeKitchenTimePerMoment.Maximum
        );
        RuleFor(g => g.SpeedModifier).InclusiveBetween(0.1, 10);
        RuleFor(g => g.MinimumItemsPerOrder)
            .InclusiveBetween((short)1, (short)250)
            .LessThanOrEqualTo(g => g.MaximumItemsPerOrder);
        RuleFor(g => g.MaximumItemsPerOrder)
            .InclusiveBetween((short)1, (short)250)
            .GreaterThanOrEqualTo(g => g.MinimumItemsPerOrder);
        RuleFor(g => g.OrderSpeedModifier).InclusiveBetween(0.1, 10);
    }
}