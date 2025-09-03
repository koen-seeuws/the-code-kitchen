using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;
using TheCodeKitchen.Cook.Contracts.Reponses.Pantry;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class Chef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();
    private readonly string _headChef;

    public Chef(string headChef, string kitchenCode, string username, string password,
        TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        _headChef = headChef;
        _theCodeKitchenClient = theCodeKitchenClient;
        Username = username;
        Password = password;
        KitchenCode = kitchenCode;

        OnKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent => { };

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
            case "EquipmentReleased":
            {
                var messageResponses = await _theCodeKitchenClient.ReadMessages();
                var messages = messageResponses
                    .Select(m => new Message(
                            m.Number,
                            m.From,
                            m.To,
                            JsonSerializer.Deserialize<MessageContent>(m.Content)!
                        )
                    )
                    .ToList();

                var lockMessage = messages.FirstOrDefault(m =>
                    m.Content.Code == "EquipmentLocked" &&
                    m.Content.EquipmentType == message.Content.EquipmentType &&
                    m.Content.EquipmentNumber == message.Content.EquipmentNumber
                );

                if (lockMessage != null)
                {
                    var confirmLockMessageRequest = new ConfirmMessageRequest(lockMessage.Number);
                    await _theCodeKitchenClient.ConfirmMessage(confirmLockMessageRequest);
                }

                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);

                break;
            }
            default:
            {
                break;
            }
        }
    }
}