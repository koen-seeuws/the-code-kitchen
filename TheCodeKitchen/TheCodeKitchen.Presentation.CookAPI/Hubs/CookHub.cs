using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class CookHub : Hub
{
    // Body must be declared in order for Azure SignalR to work correctly
}