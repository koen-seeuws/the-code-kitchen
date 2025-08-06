using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeKitchenService(HubContextProvider hubContextProvider) : IRealTimeKitchenService
{
    private const string HubName = "KitchenHub";
    
    public async Task SendNewKitchenOrderEvent(Guid kitchenId, NewKitchenOrderEvent @event)
    {
        var kitchenHubContext = await hubContextProvider.GetHubContextAsync(HubName);
        var kitchenGroup = kitchenId.ToString();
        await kitchenHubContext.Clients.Group(kitchenGroup).SendAsync(nameof(NewKitchenOrderEvent), @event);
    }
}