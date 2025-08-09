using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeKitchenService
{
    Task SendCookJoinedEvent(Guid kitchenId, CookJoinedEvent @event);
    Task SendMessageDeliveredEvent(Guid kitchenId, MessageDeliveredEvent @event);
}