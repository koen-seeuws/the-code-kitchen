using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class HeadChef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private readonly string[] _chefs;


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

            var cookFoodTasks = order.RequestedFoods.Select(async (food, index) =>
            {
                // Spreading cooking load evenly among chefs based on total requested food count
                var to = _chefs[_chefLoadCounter++ % _chefs.Length];

                // Sending the cook the order to cook the food
                var messageContent = new MessageContent(
                    "Cook Food",
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
            await ProcessMessage(message);
        };
    }

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        var messageResponses = await _theCodeKitchenClient.ReadMessages(cancellationToken);
        var messages = messageResponses
            .Select(m => new Message(
                    m.Number,
                    m.From,
                    m.To,
                    JsonSerializer.Deserialize<MessageContent>(m.Content)!
                )
            )
            .ToList();

        foreach (var message in messages)
        {
            await ProcessMessage(message);
        }
    }

    private async Task ProcessMessage(Message message)
    {
        var confirmMessageRequest = new ConfirmMessageRequest(message.Number);
        switch (message.Content.Code)
        {
            case "Food Ready":
            {
                var orders = await _theCodeKitchenClient.ViewOpenOrders();
                var order = orders.FirstOrDefault(o => o.Number == message.Content.Order!.Value);

                if (order == null)
                {
                    //Order was closed by someone else
                    await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                    return;
                }

                await _theCodeKitchenClient.TakeFoodFromEquipment(message.Content.EquipmentType!,
                    message.Content.EquipmentNumber!.Value);
                await _theCodeKitchenClient.DeliverFoodToOrder(message.Content.Order!.Value);

                var allDelivered = order.DeliveredFoods
                    .GroupBy(delivered => delivered)
                    .All(deliveredCount =>
                        order.RequestedFoods.Count(requested => requested == deliveredCount.Key) >=
                        deliveredCount.Count()
                    );

                if (allDelivered)
                {
                    await _theCodeKitchenClient.CompleteOrder(order.Number);
                }

                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case "Equipment Released" or "Equipment Locked":
            {
                // Equipment released messages can be ignored by the head chef
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
        }
    }
}