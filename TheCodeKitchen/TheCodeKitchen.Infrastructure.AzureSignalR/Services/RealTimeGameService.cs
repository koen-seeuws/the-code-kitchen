using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeGameService(HubContextProvider hubContextProvider) : IRealTimeGameService
{
    public async Task SendKitchenCreatedEvent(Guid gameId, KitchenCreatedEvent @event)
    {
        var gameGroup = GroupConstants.GetGameGroup(gameId);
        var gameHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.GameHub);
        await gameHubContext.Clients.Group(gameGroup).SendAsync(nameof(KitchenCreatedEvent), @event);
    }
    
    public async Task SendCookJoinedEvent(Guid gameId, CookJoinedEvent @event)
    {
        var gameHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.GameHub);
        var gameGroup = GroupConstants.GetGameGroup(gameId);
        await gameHubContext.Clients.Group(gameGroup).SendAsync(nameof(CookJoinedEvent), @event);
    }
}