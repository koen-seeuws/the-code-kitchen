using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Constants;
using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;
using TheCodeKitchen.Cook.Contracts.Reponses.Pantry;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;
using TheCodeKitchen.Cook.Contracts.Requests.Timer;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class Chef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();
    private readonly string _headChef;
    private readonly Dictionary<string, int> _equipments = new Dictionary<string, int>
    {
        { EquipmentType.Bbq, 6 },
        { EquipmentType.Blender, 4 },
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 10 },
        { EquipmentType.Freezer, 20 },
        { EquipmentType.Fridge, 20 },
        { EquipmentType.Fryer, 6 },
        { EquipmentType.HotPlate, 15 },
        { EquipmentType.Mixer, 6 },
        { EquipmentType.Oven, 4 },
        { EquipmentType.Stove, 6 },
    };

    public Chef(string headChef, string kitchenCode, string username, string password,
        TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        _headChef = headChef;
        _theCodeKitchenClient = theCodeKitchenClient;
        Username = username;
        Password = password;
        KitchenCode = kitchenCode;

        OnKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent =>
        {
            // Chefs do not process new orders, only the head chef does
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

        _recipes = await _theCodeKitchenClient.ReadRecipes(cancellationToken);
        _ingredients = await _theCodeKitchenClient.PantryInventory(cancellationToken);

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
            case MessageCodes.CookFood:
            {
               break;
            }
            case MessageCodes.UnlockEquipment:
            {
                var lockMessage = await FindEquipmentLockMessage(
                    message.Content.EquipmentType!,
                    message.Content.EquipmentNumber!.Value
                );

                if (lockMessage != null)
                {
                    // Lock message is used to indicate whether equipment is in use
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

    private async Task<bool> EquipmentIsLocked(string equipmentType, int equipmentNumber)
    {
        var lockMessage = await FindEquipmentLockMessage(equipmentType, equipmentNumber);

        if (lockMessage != null) return true;

        var timers = await _theCodeKitchenClient.GetTimers();
        var timer = timers.FirstOrDefault(t =>
        {
            var timerNote = JsonSerializer.Deserialize<TimerNote>(t.Note);
            return timerNote?.EquipmentType == equipmentType && timerNote.EquipmentNumber == equipmentNumber;
        });

        return timer != null;
    }


    private async Task<Message?> FindEquipmentLockMessage(string equipmentType, int equipmentNumber)
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
            m.Content.Code == MessageCodes.LockEquipment &&
            m.Content.EquipmentType == equipmentType &&
            m.Content.EquipmentNumber == equipmentNumber
        );

        return lockMessage == null
            ? null
            : new Message(lockMessage.Number, lockMessage.From, lockMessage.To, lockMessage.Content);
    }
}