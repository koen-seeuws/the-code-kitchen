using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Constants;
using TheCodeKitchen.Cook.Contracts.Events.Timer;
using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;
using TheCodeKitchen.Cook.Contracts.Reponses.Food;
using TheCodeKitchen.Cook.Contracts.Reponses.Pantry;
using TheCodeKitchen.Cook.Contracts.Requests.Communication;
using TheCodeKitchen.Cook.Contracts.Requests.Timer;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class Chef : Cook
{
    private readonly string _headChef;
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();

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
            Console.WriteLine($"{Username} - Timer Elapsed - {JsonSerializer.Serialize(timerElapsedEvent)}");
            await ProcessTimerElapsedEvent(timerElapsedEvent);
        };

        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            Console.WriteLine($"{Username} - Message Received - {JsonSerializer.Serialize(messageReceivedEvent)}");
            var message = new Message(
                messageReceivedEvent.Number,
                messageReceivedEvent.From,
                messageReceivedEvent.To,
                JsonSerializer.Deserialize<MessageContent>(messageReceivedEvent.Content)!
            );
            await ProcessMessage(message);
        };
    }

    private async Task ProcessMessage(Message message)
    {
        var confirmMessageRequest = new ConfirmMessageRequest(message.Number);
        switch (message.Content.Code)
        {
            case MessageCodes.UnlockEquipment:
            {
                var key = GetEquipmentLockKey(message.Content.EquipmentType!, message.Content.EquipmentNumber!.Value);
                _equipmentLocks[key] = false;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.LockEquipment:
            {
                var key = GetEquipmentLockKey(message.Content.EquipmentType!, message.Content.EquipmentNumber!.Value);
                _equipmentLocks[key] = true;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.CookFood:
            {
                await StartCookingFood(message.Content.Order!.Value, message.Content.Food!, []);
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            default:
            {
                break;
            }
        }
    }

    private async Task LockOrUnlockEquipment(string equipmentType, int equipmentNumber, bool isLocked)
    {
        var code = isLocked ? MessageCodes.LockEquipment : MessageCodes.UnlockEquipment;
        var messageContent = new MessageContent(code, null, null, equipmentType, equipmentNumber);
        var sendMessageRequest = new SendMessageRequest(null, JsonSerializer.Serialize(messageContent));
        await _theCodeKitchenClient.SendMessage(sendMessageRequest);
        var key = GetEquipmentLockKey(equipmentType, equipmentNumber);
        _equipmentLocks[key] = isLocked;
    }

    private int FindAvailableEquipment(string equipmentType)
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
        var key = GetEquipmentLockKey(equipmentType, equipmentNumber);
        return _equipmentLocks.GetValueOrDefault(key, false);
    }

    private string GetEquipmentLockKey(string equipmentType, int equipmentNumber)
        => $"{equipmentType}-{equipmentNumber}".ToUpper();

    private async Task RetryLater(TimerNote note, int minutes = 2)
    {
        var jsonNote = JsonSerializer.Serialize(note);
        var setTimerRequest = new SetTimerRequest(TimeSpan.FromMinutes(minutes), jsonNote);
        await _theCodeKitchenClient.SetTimer(setTimerRequest);
    }

    private async Task StartCookingFood(long orderNumber, string food, string[] ingredientOfTree)
    {
        //TODO Put ingredients in equipment and set timers their timers
        //If correct equipment not available -> Retry later
    }

    private async Task ProcessTimerElapsedEvent(TimerElapsedEvent timerElapsedEvent)
    {
        // Immediately stop the timer, it keeps firing each moment until stopped, we will set new timer if needed
        var stopTimerRequest = new StopTimerRequest(timerElapsedEvent.Number);
        await _theCodeKitchenClient.StopTimer(stopTimerRequest);

        if (timerElapsedEvent.Note == null) return;
        var timerNote = JsonSerializer.Deserialize<TimerNote>(timerElapsedEvent.Note);
        if (timerNote == null) return;

        if (_currentFoodInHands != null)
        {
            // If already holding food -> Ongoing action and try again 2 minutes later
            await RetryLater(timerNote);
            return;
        }

        var sourceEquipmentType = timerNote.EquipmentType;
        var sourceEquipmentNumber = timerNote.EquipmentNumber;

        var allStepsDone = !timerNote.StepsToDo.Any();

        // Not all steps done -> Simply put it in the next necessary equipment
        if (!allStepsDone)
        {
            var stepToDo = timerNote.StepsToDo.First();
            var destinationEquipmentType = stepToDo.EquipmentType;
            var destinationEquipmentNumber = FindAvailableEquipment(destinationEquipmentType);
            if (destinationEquipmentNumber <= 0)
            {
                // No equipment available -> Try again in 2 minutes
                await RetryLater(timerNote);
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
        var isRequestedByHeadChef = timerNote.RecipeTree.Length <= 0;
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

        // TODO: All steps are done and is part of recipe -> Add all ingredients in 1 equipment and take food from there
        // If the recipe has steps -> The all the necessary ingredients in this equipment and take food from there after the necessary step time
        // If the recipe has no steps -> Put all the necessary ingredients in 1 EquipmentType.Counter and take from there (or maybe send HeadChef directly?)
    }
}