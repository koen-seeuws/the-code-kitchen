using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Results;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public class PauseOrUnpauseGameCommandHandler(
    IGameRepository gameRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IMapper mapper
) : IRequestHandler<PauseOrUnpauseGameCommand, Result<PauseOrUnpauseGameResponse>>
{
    public async Task<Result<PauseOrUnpauseGameResponse>> Handle(PauseOrUnpauseGameCommand request,
        CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetByIdAsync(request.GameId, cancellationToken);
        game.PauseOrUnpauseGame();
        
        //TODO: actually start and stop the generation of orders
        
        await gameRepository.UpdateAsync(game, cancellationToken);
        await domainEventDispatcher.DispatchAndClearEvents(game, cancellationToken);
        var pauseOrUnpauseGameResponse = mapper.Map<PauseOrUnpauseGameResponse>(game);
        return pauseOrUnpauseGameResponse;
    }
}