using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeKitchenOrderService
{
    Task SendNewKitchenOrderEvent(Guid kitchenId, NewKitchenOrderEvent @event);
    Task SendKitchenOrderCompletedEvent(Guid kitchenId, KitchenOrderCompletedEvent @event);
    Task SendKitchenOrderFoodDeliveredEvent(Guid kitchenId, KitchenOrderFoodDeliveredEvent @event);
}