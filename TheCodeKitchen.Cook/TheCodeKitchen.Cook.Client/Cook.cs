using System.Text.Json;
using TheCodeKitchen.Cook.Contracts.Events.Communication;
using TheCodeKitchen.Cook.Contracts.Events.Order;
using TheCodeKitchen.Cook.Contracts.Events.Timer;

namespace TheCodeKitchen.Cook.Client;

public class Cook(string baseUrl)
{
    private const string KitchenCode = "6SYE"; //TODO: Replace with your kitchen code
    private const string Username = "YYYY"; //TODO: Replace with your username
    private const string Password = "ZZZZ"; //TODO: Replace with your password

    private readonly TheCodeKitchenClient _client = new(Username, Password, KitchenCode, baseUrl);

    private readonly Action<KitchenOrderCreatedEvent> _onKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent =>
    {
        // TODO: Implement logic here
    };

    private readonly Action<TimerElapsedEvent> _onTimerElapsedEvent = async timerElapsedEvent =>
    {
        // TODO: Implement logic here
    };

    private readonly Action<MessageReceivedEvent> _onMessageReceivedEvent = async messageReceivedEvent =>
    {
        // TODO: Implement logic here
    };

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