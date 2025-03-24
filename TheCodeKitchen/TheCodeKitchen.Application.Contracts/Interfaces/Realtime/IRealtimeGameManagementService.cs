using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealtimeGameManagementService
{
    Task GameCreated(GameCreatedEventDto gameCreatedEvent);
    Task KitchenCreated(Guid gameId, KitchenAddedEventDto kitchenAddedEvent);
    Task CookJoined(Guid gameId, CookJoinedEventDto cookJoinedEvent);
}