using TheCodeKitchen.Application.Contracts.Events;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeKitchenService
{
    Task SendNewKitchenOrderEvent(Guid kitchenId, NewKitchenOrderEvent @event);
}