using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeGameService
{
    Task SendKitchenCreatedEvent(Guid gameId, KitchenCreatedEvent @event);
}