using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events.GameManagement;
using TheCodeKitchen.Application.Contracts.Realtime;
using TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeGameManagementService(HubContextProvider hubContextProvider) : IRealTimeGameManagementService
{
    public async Task SendGameCreatedEvent(GameCreatedEvent @event)
    {
        var gameManagementHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.GameManagementHub);
        await gameManagementHubContext.Clients.All.SendAsync(nameof(GameCreatedEvent), @event);
    }
}