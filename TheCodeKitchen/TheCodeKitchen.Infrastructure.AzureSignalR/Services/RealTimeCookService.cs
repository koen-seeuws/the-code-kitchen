using Microsoft.AspNetCore.SignalR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

namespace TheCodeKitchen.Infrastructure.AzureSignalR.Services;

public class RealTimeCookService(HubContextProvider hubContextProvider) : IRealTimeCookService
{
    private const string HubName = "CookHub";
    
    public async Task SendMessageReceivedEvent(MessageReceivedEvent @event)
    {
        var cookHubContext = await hubContextProvider.GetHubContextAsync(HubName);
        await cookHubContext.Clients.User(@event.To).SendAsync(nameof(MessageReceivedEvent), @event);
    }

    public async Task SendTimerElapsedEvent(string username, TimerElapsedEvent @event)
    {
        var cookHubContext = await hubContextProvider.GetHubContextAsync(HubName);
        await cookHubContext.Clients.User(username).SendAsync(nameof(TimerElapsedEvent), @event);
    }
}