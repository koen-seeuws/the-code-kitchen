using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Infrastructure.SignalR.Services;

public class GameManagementSignalRService(
    IHubContext<GameManagementHub> gameManagementHub
) : IRealtimeGameManagementService
{
    public async Task GameCreated(GameCreatedEventDto gameCreatedEvent)
        => await gameManagementHub.Clients.All.SendAsync("GameCreated", gameCreatedEvent);
}