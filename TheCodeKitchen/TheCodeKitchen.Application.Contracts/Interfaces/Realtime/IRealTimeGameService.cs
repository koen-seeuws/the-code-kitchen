using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeGameService
{
    Task SendKitchenCreatedEvent(Guid gameId, KitchenCreatedEvent @event);
    Task SendCookJoinedEvent(Guid gameId, CookJoinedEvent @event);
}