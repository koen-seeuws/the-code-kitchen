using TheCodeKitchen.Application.Contracts.Requests.Game;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Application.Business.Validators;

public class CreateGameValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameValidator()
    {
        RuleFor(g => g.Name).NotEmpty();
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