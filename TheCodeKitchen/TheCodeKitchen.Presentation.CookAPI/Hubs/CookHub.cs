using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TheCodeKitchen.Presentation.API.Cook.Hubs;

[Authorize]
public class CookHub : Hub;