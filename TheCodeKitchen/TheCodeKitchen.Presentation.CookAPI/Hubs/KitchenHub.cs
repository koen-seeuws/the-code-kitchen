using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Infrastructure.Security.Extensions;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class KitchenHub(
    IHubContext<KitchenHub> hubContext,
    StreamSubscriber<Guid, NextMomentEvent> nextMomentStreamSubscriber
) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var gameId = Context.User?.GetGameId() ?? throw new UnauthorizedAccessException();

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await nextMomentStreamSubscriber.SubscribeToEvents(gameId,
            async @event =>
            {
                await hubContext.Clients.Group(gameId.ToString()).SendAsync(nameof(NextMomentEvent), @event);
            }
        );

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var gameId = Context.User?.GetGameId() ?? throw new UnauthorizedAccessException();

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        await nextMomentStreamSubscriber.UnsubscribeFromEvents(gameId);

        await base.OnDisconnectedAsync(exception);
    }
}