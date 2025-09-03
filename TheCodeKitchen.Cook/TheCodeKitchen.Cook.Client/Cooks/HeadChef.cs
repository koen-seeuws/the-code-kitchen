using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class HeadChef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private readonly string[] _chefs;

    private ICollection<Message> _messages = new List<Message>();
    private ICollection<Order> _orders = new List<Order>();

    private int _chefLoadCounter;

    public HeadChef(string[] chefs, string kitchenCode, string username, string password,
        TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        _chefs = chefs;
        _theCodeKitchenClient = theCodeKitchenClient;
        Username = username;
        Password = password;
        KitchenCode = kitchenCode;

        OnKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent =>
        {
            var order = new Order(
                kitchenOrderCreatedEvent.Number,
                kitchenOrderCreatedEvent.RequestedFoods,
                []
            );

            Console.WriteLine($"{Username} - New Order Received - {JsonSerializer.Serialize(order)}");

            _orders.Add(order);

            var cookFoodTasks = order.RequestedFoods.Select(async (food, index) =>
            {
                // Spreading cooking load evenly among chefs based on total requested food count
                var to = _chefs[_chefLoadCounter++ % _chefs.Length];

                // Sending the cook the order to cook the food
                var messageContent = new MessageContent(
                    "CookFood",
                    order.Number,
                    food,
                    null,
                    null
                );
                var sendMessage = new SendMessageRequest(to, JsonSerializer.Serialize(messageContent));
                await _theCodeKitchenClient.SendMessage(sendMessage);
            });

            await Task.WhenAll(cookFoodTasks);
        };

        OnTimerElapsedEvent = async timerElapsedEvent => { };

        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            var message = new Message(
                messageReceivedEvent.Number,
                messageReceivedEvent.From,
                messageReceivedEvent.To,
                JsonSerializer.Deserialize<MessageContent>(messageReceivedEvent.Content)!
            );
            Console.WriteLine($"{Username} - Message Received - {JsonSerializer.Serialize(message)}");
            _messages.Add(message);
        };
    }

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        var messages = await _theCodeKitchenClient.ReadMessages(cancellationToken);
        _messages = messages
            .Select(m => new Message(
                    m.Number,
                    m.From,
                    m.To,
                    JsonSerializer.Deserialize<MessageContent>(m.Content)!
                )
            )
            .ToList();

        var orders = await _theCodeKitchenClient.ViewOpenOrders(cancellationToken);
        _orders = orders
            .Select(o => new Order(
                    o.Number,
                    o.RequestedFoods,
                    o.DeliveredFoods
                )
            )
            .ToList();
    }
}