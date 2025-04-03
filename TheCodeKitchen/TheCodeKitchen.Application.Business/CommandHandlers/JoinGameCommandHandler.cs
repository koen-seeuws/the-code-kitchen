using System.Net;
using TheCodeKitchen.Application.Contracts.Interfaces.Authentication;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public class JoinGameCommandHandler(
    IGameRepository gameRepository,
    ICookRepository cookRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IAuthenticationService authenticationService
) : IRequestHandler<JoinGameCommand, Result<JoinGameResponse>>
{
    public async Task<Result<JoinGameResponse>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        => await TryAsync(gameRepository.GetGameWithKitchensAndCooksByCode(request.KitchenCode, cancellationToken))
            .Map(game => (game, passwordHash: authenticationService.HashPassword(request.Password)))
            .Map(result => (result.game,
                cook: result.game.JoinGame(request.KitchenCode, request.Username, result.passwordHash)))
            .Bind(result =>
                TryAsync(cookRepository.UpdateAsync(result.cook, cancellationToken))
                    .Map(_ => (result.game,result.cook))
            )
            .Do(result => domainEventDispatcher.DispatchEvents(result.game.GetEvents()))
            .Do(result => result.game.ClearEvents())
            .Map(result => authenticationService.GeneratePlayerToken(result.cook.KitchenId, result.cook.Username))
            .Map(token => new JoinGameResponse(token))
            .Invoke();
}