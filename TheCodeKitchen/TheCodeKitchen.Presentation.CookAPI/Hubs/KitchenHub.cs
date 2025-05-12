using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Infrastructure.Security.Extensions;
using TheCodeKitchen.Presentation.WebCore;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class KitchenHub(
    IHubContext<KitchenHub> hubContext,
    StreamSubscriber<Guid, NextMomentEvent> nextMomentStreamSubscriber
) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();

        await Groups.AddToGroupAsync(Context.ConnectionId, kitchenId.ToString());
        await nextMomentStreamSubscriber.SubscribeToEvents(kitchenId,
            async @event =>
            {
                await hubContext.Clients.Group(kitchenId.ToString()).SendAsync(nameof(NextMomentEvent), @event);
            }
        );

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var kitchenId = Context.User?.GetKitchenId() ?? throw new UnauthorizedAccessException();

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, kitchenId.ToString());
        await nextMomentStreamSubscriber.UnsubscribeFromEvents(kitchenId);

        await base.OnDisconnectedAsync(exception);
    }
}