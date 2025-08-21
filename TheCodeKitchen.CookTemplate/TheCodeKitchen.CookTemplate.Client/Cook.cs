using System.Text.Json;
using TheCodeKitchen.CookTemplate.Contracts.Events.Communication;
using TheCodeKitchen.CookTemplate.Contracts.Events.Order;
using TheCodeKitchen.CookTemplate.Contracts.Events.Timer;

namespace TheCodeKitchen.CookTemplate.Client;

public class Cook(string username, string password, string kitchenCode, string baseUrl)
{
    private readonly TheCodeKitchenClient _client = new(username, password, kitchenCode, baseUrl);

    private readonly Action<KitchenOrderCreatedEvent> _onKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent =>
    {
    };

    private readonly Action<TimerElapsedEvent> _onTimerElapsedEvent = async timerElapsedEvent => { };

    private readonly Action<MessageReceivedEvent> _onMessageReceivedEvent = async messageReceivedEvent => { };

    public async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await _client.Connect(
            _onKitchenOrderCreatedEvent,
            _onTimerElapsedEvent,
            _onMessageReceivedEvent,
            cancellationToken
        );
    }
}