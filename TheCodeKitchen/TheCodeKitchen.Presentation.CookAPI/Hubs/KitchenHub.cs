using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class KitchenHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();

        await Groups.AddToGroupAsync(Context.ConnectionId, kitchenId.ToString());
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, kitchenId.ToString());

        await base.OnDisconnectedAsync(exception);
    }
}