using System.Text.Json;
using System.Threading.Channels;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class HeadChef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private readonly string[] _chefs;
    private int _chefLoadCounter;
    private readonly Channel<Message> _messages = Channel.CreateUnbounded<Message>();

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
            Console.WriteLine(
                $"{Username} - New Order Received - {JsonSerializer.Serialize(kitchenOrderCreatedEvent)}");

            var cookFoodTasks = kitchenOrderCreatedEvent.RequestedFoods.Select(async (food, index) =>
            {
                // Spreading cooking load evenly among chefs
                var to = _chefs[_chefLoadCounter++ % _chefs.Length];

                // Sending the cook the order to cook the food
                var messageContent = new MessageContent(
                    MessageCodes.CookFood,
                    kitchenOrderCreatedEvent.Number,
                    food,
                    null,
                    null
                );
                var sendMessage = new SendMessageRequest(to, JsonSerializer.Serialize(messageContent));
                await _theCodeKitchenClient.SendMessage(sendMessage);
            });

            await Task.WhenAll(cookFoodTasks);
        };

        OnTimerElapsedEvent = async timerElapsedEvent =>
        {
            // Head chef does not process timer events, since chefs handle cooking
        };

        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            var message = new Message(
                messageReceivedEvent.Number,
                messageReceivedEvent.From,
                messageReceivedEvent.To,
                JsonSerializer.Deserialize<MessageContent>(messageReceivedEvent.Content)!
            );
            Console.WriteLine($"{Username} - Message Received - {JsonSerializer.Serialize(message)}");

            await _messages.Writer.WriteAsync(message);
        };
    }

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        _ = Task.Run(async () =>
        {
            await foreach (var message in _messages.Reader.ReadAllAsync(cancellationToken))
            {
                try
                {
                    await ProcessMessage(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }, cancellationToken);
    }

    private async Task ProcessMessage(Message message)
    {
        var confirmMessageRequest = new ConfirmMessageRequest(message.Number);
        switch (message.Content.Code)
        {
            case MessageCodes.FoodReady:
            {
                var orders = await _theCodeKitchenClient.ViewOpenOrders();
                var order = orders.FirstOrDefault(o => o.Number == message.Content.Order!.Value);

                if (order == null)
                {
                    //Order was closed by someone else
                    await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                    return;
                }

                Console.WriteLine(
                    $"{Username} - ProcessMessage - Delivering {message.Content.Food} to order {message.Content.Order!.Value}");

                await _theCodeKitchenClient.TakeFoodFromEquipment(message.Content.EquipmentType!,
                    message.Content.EquipmentNumber!.Value);
                await UnlockEquipment(message.Content.EquipmentType!, message.Content.EquipmentNumber!.Value);
                await _theCodeKitchenClient.DeliverFoodToOrder(message.Content.Order!.Value);

                var deliveredGroups = order.DeliveredFoods
                    .Append(message.Content.Food)
                    .GroupBy(f => f!.Trim().ToLowerInvariant())
                    .ToDictionary(g => g.Key, g => g.Count());

                var allDelivered = order.RequestedFoods
                    .GroupBy(f => f.Trim().ToLowerInvariant())
                    .All(req => deliveredGroups.TryGetValue(req.Key, out var deliveredCount) &&
                                deliveredCount >= req.Count());

                if (allDelivered)
                {
                    Console.WriteLine($"{Username} - ProcessMessage - Completing order {message.Content.Order!.Value}");
                    await _theCodeKitchenClient.CompleteOrder(message.Content.Order!.Value);
                }

                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.LockEquipment or MessageCodes.UnlockEquipment:
            {
                // Equipment lock/unlock messages can be ignored by the head chef
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
        }
    }

    private async Task UnlockEquipment(string equipmentType, int equipmentNumber)
    {
        var messageContent =
            new MessageContent(MessageCodes.UnlockEquipment, null, null, equipmentType, equipmentNumber);
        var messageContentJson = JsonSerializer.Serialize(messageContent);
        var sendMessageRequest = new SendMessageRequest(null, messageContentJson);
        await _theCodeKitchenClient.SendMessage(sendMessageRequest);
    }
}