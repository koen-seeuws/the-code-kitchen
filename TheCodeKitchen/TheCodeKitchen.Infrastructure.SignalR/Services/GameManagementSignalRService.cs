using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Infrastructure.SignalR.Services;

public class GameManagementSignalRService(
    IHubContext<GameManagementHub> gameManagementHub
) : IRealtimeGameManagementService
{
    public async Task GameCreated(GameCreatedEventDto gameCreatedEvent)
        => await gameManagementHub.Clients.All.SendAsync(nameof(GameCreated), gameCreatedEvent);

    public async Task KitchenCreated(Guid gameId, KitchenAddedEventDto kitchenAddedEvent)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(KitchenCreated), kitchenAddedEvent);
    
    public async Task CookJoined(Guid gameId, CookJoinedEventDto cookJoinedEvent)
        => await gameManagementHub.Clients
            .Group($"game-{gameId}")
            .SendAsync(nameof(CookJoined), cookJoinedEvent);
}