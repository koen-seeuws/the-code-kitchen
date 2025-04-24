using TheCodeKitchen.Application.Business.Services;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class AddKitchenCommandValidator : AbstractValidator<AddKitchenCommand>
{
    public AddKitchenCommandValidator()
    {
        RuleFor(kitchen => kitchen.Name)
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(kitchen => kitchen.GameId).NotEmpty();
    }
}

public sealed class AddKitchenCommandHandler(
    IGameRepository gameRepository,
    IKitchenRepository kitchenRepository,
    IKitchenService kitchenService,
    IDomainEventDispatcher domainEventDispatcher,
    IMapper mapper
) : IRequestHandler<AddKitchenCommand, Result<AddKitchenResponse>>
{
    public async Task<Result<AddKitchenResponse>> Handle(
        AddKitchenCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var game = await gameRepository.GetGameWithKitchensById(request.GameId, cancellationToken);
        var code = await kitchenService.GenerateUniqueCode(cancellationToken: cancellationToken);
        var kitchen = game.AddKitchen(request.Name, code);
        await kitchenRepository.AddAsync(kitchen, cancellationToken);
        await domainEventDispatcher.DispatchAndClearEvents(game, cancellationToken);
        var addKitchenResponse = mapper.Map<AddKitchenResponse>(kitchen);
        return addKitchenResponse;
    }
}