using TheCodeKitchen.Application.Contracts.Events.Kitchen;

namespace TheCodeKitchen.Infrastructure.SignalR.Services;

public class KitchenManagementSignalRService(
    IHubContext<KitchenManagementHub> kitchenManagementHubContext
) : IRealtimeKitchenManagementService
{
    public async Task KitchenCreated(Guid gameId, KitchenCreatedEventDto kitchenCreatedEvent)
    => await kitchenManagementHubContext.Clients
            .Group($"game-{gameId}")
            .SendAsync("KitchenCreated", kitchenCreatedEvent);
    
}