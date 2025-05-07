using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Infrastructure.Security.Extensions;
using TheCodeKitchen.Presentation.API.Cook.StreamSubscribers;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class KitchenHub(NextMomentStreamSubscriber nextMomentStreamSubscriber) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();
        
        await Groups.AddToGroupAsync(Context.ConnectionId, kitchenId.ToString());
        await nextMomentStreamSubscriber.SubscribeToKitchenEvents(kitchenId);
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, kitchenId.ToString());
        await nextMomentStreamSubscriber.UnSubscribeFromKitchenEvents(kitchenId);
        
        await base.OnDisconnectedAsync(exception);
    }
    

}