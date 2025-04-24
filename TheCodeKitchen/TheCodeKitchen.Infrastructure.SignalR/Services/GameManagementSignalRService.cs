using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Infrastructure.SignalR.Services;

public class GameManagementSignalRService(
    IHubContext<GameManagementHub> gameManagementHub
) : IRealtimeGameManagementService
{
    public async Task GameCreated(GameCreatedEventDto gameCreatedEvent, CancellationToken cancellationToken = default)
        => await gameManagementHub.Clients.All.SendAsync(nameof(GameCreated), gameCreatedEvent,
            cancellationToken: cancellationToken);

    public async Task KitchenCreated(Guid gameId, KitchenAddedEventDto kitchenAddedEvent,
        CancellationToken cancellationToken = default)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(KitchenCreated), kitchenAddedEvent, cancellationToken: cancellationToken);

    public async Task CookJoined(Guid gameId, CookJoinedEventDto cookJoinedEvent,
        CancellationToken cancellationToken = default)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(CookJoined), cookJoinedEvent, cancellationToken: cancellationToken);

    public async Task GameStarted(Guid gameId, CancellationToken cancellationToken = default)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(GameStarted), cancellationToken: cancellationToken);

    public async Task GamePausedOrUnpausedNotification(Guid gameId, GamePausedOrUnpausedEventDto gamePausedOrUnpausedEvent, CancellationToken cancellationToken = default)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(GamePausedOrUnpausedNotification), gamePausedOrUnpausedEvent, cancellationToken: cancellationToken);
}