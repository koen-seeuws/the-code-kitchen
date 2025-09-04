using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Constants;
using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;
using TheCodeKitchen.Cook.Contracts.Reponses.Food;
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

    private TakeFoodResponse? _currentFoodInHands = null;
    private Dictionary<string, bool> _equipmentLocks = new();

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

        OnTimerElapsedEvent = async timerElapsedEvent =>
        {
            var stopTimerRequest = new StopTimerRequest(timerElapsedEvent.Number);
            await _theCodeKitchenClient.StopTimer(stopTimerRequest);

            if (timerElapsedEvent.Note == null) return;
            var timerNote = JsonSerializer.Deserialize<TimerNote>(timerElapsedEvent.Note);
            if (timerNote == null) return;

            var sourceEquipmentType = timerNote.EquipmentType;
            var sourceEquipmentNumber = timerNote.EquipmentNumber;

            if (_currentFoodInHands != null)
            {
                // If already holding food -> Ongoing action and try again 2 minutes later
                await RetryLater(timerElapsedEvent.Note);
                return;
            }

            var allStepsDone = !timerNote.StepsToDo.Any();

            // Not all steps done -> Simply put it in the next equipment
            if (!allStepsDone)
            {
                var stepToDo = timerNote.StepsToDo.First();
                var destinationEquipmentType = stepToDo.EquipmentType;
                var destinationEquipmentNumber = FindUnlockedEquipment(destinationEquipmentType);
                if (destinationEquipmentNumber <= 0)
                {
                    // No equipment available -> Try again in 2 minutes
                    await RetryLater(timerElapsedEvent.Note);
                    return;
                }

                // Lock destination equipment, take food, unlock source equipment
                await LockOrUnlockEquipment(destinationEquipmentType, destinationEquipmentNumber, true);

                if (sourceEquipmentType != null && sourceEquipmentNumber.HasValue)
                {
                    // Food is in equipment -> Take from there
                    _currentFoodInHands = await _theCodeKitchenClient.TakeFoodFromEquipment(
                        sourceEquipmentType, sourceEquipmentNumber.Value);
                    await LockOrUnlockEquipment(sourceEquipmentType, sourceEquipmentNumber.Value, false);
                }
                else
                {
                    // Food is in pantry -> Take from there
                    _currentFoodInHands = await _theCodeKitchenClient.TakeFoodFromPantry(timerNote.Food);
                }

                // Add food to destination equipment, set timer for next step, update note for next step
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
                _currentFoodInHands = null;
                
                var timerNoteForNextStep = timerNote with
                {
                    EquipmentType = destinationEquipmentType,
                    EquipmentNumber = destinationEquipmentNumber,
                    StepsToDo = timerNote.StepsToDo.Skip(1).ToList()
                };
                var timerNoteForNextStepJson = JsonSerializer.Serialize(timerNoteForNextStep);
                
                var setTimerRequest = new SetTimerRequest(stepToDo.Time, timerNoteForNextStepJson);
                await _theCodeKitchenClient.SetTimer(setTimerRequest);
                return;
            }

            // Notify head chef that food is ready -> he will take it straight from the equipment
            var isRequestedByHeadChef = timerNote.RecipeTree.Length == 0;
            if (isRequestedByHeadChef)
            {
                var messageContent = new MessageContent(
                    MessageCodes.FoodReady,
                    timerNote.Order,
                    timerNote.Food,
                    sourceEquipmentType,
                    sourceEquipmentNumber
                );
                var sendMessage = new SendMessageRequest(_headChef, JsonSerializer.Serialize(messageContent));
                await _theCodeKitchenClient.SendMessage(sendMessage);
                return;
            }

            // TODO: Is part of recipe -> Merge with other ingredients in 1 equipment
            
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
                await StartCookFood(message.Content.Order!.Value, message.Content.Food!);
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.UnlockEquipment:
            {
                var key = $"{message.Content.EquipmentType}-{message.Content.EquipmentNumber}".ToLower();
                _equipmentLocks[key] = false;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.LockEquipment:
            {
                var key = $"{message.Content.EquipmentType}-{message.Content.EquipmentNumber}".ToLower();
                _equipmentLocks[key] = true;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            default:
            {
                break;
            }
        }
    }

    private async Task StartCookFood(long orderNumber, string food)
    {
        //TODO -> Put ingredients in equipment and set timers
    }

    private async Task LockOrUnlockEquipment(string equipmentType, int equipmentNumber, bool isLocked)
    {
        var code = isLocked ? MessageCodes.LockEquipment : MessageCodes.UnlockEquipment;
        var messageContent = new MessageContent(
            MessageCodes.LockEquipment, null, null, equipmentType, equipmentNumber);
        var sendMessageRequest = new SendMessageRequest(null, JsonSerializer.Serialize(messageContent));
        await _theCodeKitchenClient.SendMessage(sendMessageRequest);
        var key = $"{equipmentType}-{equipmentNumber}".ToLower();
        _equipmentLocks[key] = isLocked;
    }

    private int FindUnlockedEquipment(string equipmentType)
    {
        var equipmentCount = _equipments.GetValueOrDefault(equipmentType, 0);
        for (var equipmentNumber = 1; equipmentNumber <= equipmentCount; equipmentNumber++)
        {
            if (!EquipmentIsLocked(equipmentType, equipmentNumber))
                return equipmentNumber;
        }

        return -1;
    }

    private bool EquipmentIsLocked(string equipmentType, int equipmentNumber)
    {
        var key = $"{equipmentType}-{equipmentNumber}".ToLower();
        return _equipmentLocks.GetValueOrDefault(key, false);
    }

    private async Task RetryLater(string note, int minutes = 2)
    {
        var setTimerRequest = new SetTimerRequest(TimeSpan.FromMinutes(minutes), note);
        await _theCodeKitchenClient.SetTimer(setTimerRequest);
    }
}