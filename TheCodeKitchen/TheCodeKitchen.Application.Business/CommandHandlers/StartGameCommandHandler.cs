using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Results;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public class StartGameCommandHandler(
    IGameRepository gameRepository,
    IDomainEventDispatcher domainEventDispatcher
) : IRequestHandler<StartGameCommand, Result<TheCodeKitchenUnit>>
{
    public async Task<Result<TheCodeKitchenUnit>> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetGameWithKitchensById(request.GameId, cancellationToken);
        game.StartGame();
        await gameRepository.UpdateAsync(game, cancellationToken);
        await domainEventDispatcher.DispatchAndClearEvents(game, cancellationToken);
        return TheCodeKitchenUnit.Value;
    }
}