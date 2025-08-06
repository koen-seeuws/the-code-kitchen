using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeCookService(HubContextProvider hubContextProvider) : IRealTimeCookService
{ 
    public async Task SendMessageReceivedEvent(MessageReceivedEvent @event)
    {
        var cookHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.CookHub);
        await cookHubContext.Clients.User(@event.To).SendAsync(nameof(MessageReceivedEvent), @event);
    }

    public async Task SendTimerElapsedEvent(string username, TimerElapsedEvent @event)
    {
        var cookHubContext = await hubContextProvider.GetHubContextAsync(HubConstants.CookHub);
        await cookHubContext.Clients.User(username).SendAsync(nameof(TimerElapsedEvent), @event);
    }
}