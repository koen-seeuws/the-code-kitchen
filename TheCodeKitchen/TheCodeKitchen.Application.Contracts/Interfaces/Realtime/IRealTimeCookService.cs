using TheCodeKitchen.Application.Contracts.Events;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

public interface IRealTimeCookService
{
    Task SendMessageReceivedEvent(MessageReceivedEvent @event);
    Task SendTimerElapsedEvent(string username, TimerElapsedEvent @event);
}