using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeKitchenService(HubContextProvider hubContextProvider) : IRealTimeKitchenService
{
    public async Task SendKitchenRatingUpdatedEvent(Guid kitchenId, KitchenRatingUpdatedEvent @event)
    {
        var kitchenHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.KitchenHub);
        var kitchenGroup = GroupConstants.GetKitchenGroup(kitchenId);
        await kitchenHubContext.Clients.Group(kitchenGroup).SendAsync(nameof(KitchenRatingUpdatedEvent), @event);
    }

    public async Task SendMessageDeliveredEvent(Guid kitchenId, MessageDeliveredEvent @event)
    {
        var kitchenHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.KitchenHub);
        var kitchenGroup = GroupConstants.GetKitchenGroup(kitchenId);
        await kitchenHubContext.Clients.Group(kitchenGroup).SendAsync(nameof(MessageDeliveredEvent), @event);
    }
}