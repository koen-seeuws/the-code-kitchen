using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealtimeKitchenManagementService
{
    Task KitchenCreated(Guid gameId, KitchenCreatedEventDto kitchenCreatedEvent);
}