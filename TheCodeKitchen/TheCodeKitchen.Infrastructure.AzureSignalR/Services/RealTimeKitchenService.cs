using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeKitchenService(HubContextProvider hubContextProvider) : IRealTimeKitchenService
{
    public async Task SendNewKitchenOrderEvent(Guid kitchenId, NewKitchenOrderEvent @event)
    {
        var kitchenHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.CookHub);
        var kitchenGroup = GroupConstants.GetKitchenGroup(kitchenId);
        await kitchenHubContext.Clients.Group(kitchenGroup).SendAsync(nameof(NewKitchenOrderEvent), @event);
    }
}