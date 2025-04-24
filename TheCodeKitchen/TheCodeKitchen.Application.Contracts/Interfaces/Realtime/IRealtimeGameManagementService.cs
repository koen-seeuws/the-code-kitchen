using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealtimeGameManagementService
{
    Task GameCreated(GameCreatedEventDto gameCreatedEvent, CancellationToken cancellationToken = default);
    Task KitchenCreated(Guid gameId, KitchenAddedEventDto kitchenAddedEvent, CancellationToken cancellationToken = default);
    Task CookJoined(Guid gameId, CookJoinedEventDto cookJoinedEvent, CancellationToken cancellationToken = default);
    Task GameStarted(Guid gameId, CancellationToken cancellationToken = default);
    Task GamePausedOrUnpausedNotification(Guid gameId, GamePausedOrUnpausedEventDto gamePausedOrUnpausedEvent, CancellationToken cancellationToken = default);
}