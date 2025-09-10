using System.Collections.Concurrent;
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
    private readonly string _headChef;
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();

    private int _foodId = 1;
    private readonly ConcurrentDictionary<string, bool> _equipmentLocks = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<int> _timersProcessed = new();
    private readonly PriorityQueue<TimerNote, int> _timerEventQueue = new();

    private readonly ConcurrentDictionary<string, Tuple<string, int>> _preparedIngredientLocations =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly Dictionary<string, int> _equipments = new(StringComparer.OrdinalIgnoreCase)
    {
        { EquipmentType.Bbq, 8 },
        { EquipmentType.Blender, 4 },
        { EquipmentType.Counter, 30 },
        { EquipmentType.CuttingBoard, 10 },
        { EquipmentType.Freezer, 20 },
        { EquipmentType.Fridge, 20 },
        { EquipmentType.Fryer, 6 },
        { EquipmentType.HotPlate, 15 },
        { EquipmentType.Mixer, 6 },
        { EquipmentType.Oven, 6 },
        { EquipmentType.Stove, 8 },
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
            // Immediately stop the timer, it keeps firing each moment until stopped, we will set new timer if needed
            var stopTimerRequest = new StopTimerRequest(timerElapsedEvent.Number);
            await _theCodeKitchenClient.StopTimer(stopTimerRequest);

            if (_timersProcessed.Contains(timerElapsedEvent.Number))
            {
                Console.WriteLine($"{Username} - Timer Elapsed already processed - {timerElapsedEvent.Number}");
                return;
            }

            _timersProcessed.Add(timerElapsedEvent.Number);
            var note = JsonSerializer.Deserialize<TimerNote>(timerElapsedEvent.Note!);
            lock (_timerEventQueue)
            {
                _timerEventQueue.Enqueue(note!, note!.Id);
            }
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

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        _recipes = await _theCodeKitchenClient.ReadRecipes(cancellationToken);
        _ingredients = await _theCodeKitchenClient.PantryInventory(cancellationToken);

        _ = Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                TimerNote note;
                lock (_timerEventQueue)
                {
                    if (_timerEventQueue.Count == 0)
                        continue;
                    note = _timerEventQueue.Dequeue();
                }

                try
                {
                    await ProcessTimerElapsedEvent(note);
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
            case MessageCodes.UnlockEquipment:
            {
                Console.WriteLine(
                    $"{Username} - Process Message - Unlocking Equipment - {message.Content.EquipmentType!} {message.Content.EquipmentNumber!.Value}");
                var key = GetEquipmentLockKey(message.Content.EquipmentType!, message.Content.EquipmentNumber!.Value);
                _equipmentLocks[key] = false;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.LockEquipment:
            {
                Console.WriteLine(
                    $"{Username} - Process Message - Locking Equipment - {message.Content.EquipmentType!} {message.Content.EquipmentNumber!.Value}");
                var key = GetEquipmentLockKey(message.Content.EquipmentType!, message.Content.EquipmentNumber!.Value);
                _equipmentLocks[key] = true;
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
            case MessageCodes.CookFood:
            {
                Console.WriteLine(
                    $"{Username} - Process Message - Cooking Food - Order: {message.Content.Order!.Value}, Food: {message.Content.Food!}");
                await StartCookingIngredients(message.Content.Order!.Value, message.Content.Food!, [], _foodId++);
                await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
                break;
            }
        }
    }

    private async Task LockOrUnlockEquipment(string equipmentType, int equipmentNumber, bool isLocked)
    {
        Console.WriteLine(
            $"{Username} - LockOrUnlockEquipment - {(isLocked ? "Locking" : "Unlocking")} Equipment - {equipmentType} {equipmentNumber}");
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
        if (equipmentCount == 0)
            return -1;

        // Create a shuffled list of equipment numbers (to reduce chance of collisions)
        var equipmentNumbers = Enumerable
            .Range(0, equipmentCount)
            .OrderBy(_ => Random.Shared.Next());

        foreach (var equipmentNumber in equipmentNumbers)
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

    private async Task StartCookingIngredients(long orderNumber, string food, string[] ingredientOfTree, int id)
    {
        var recipe = _recipes.First(r => r.Name.Equals(food, StringComparison.OrdinalIgnoreCase));

        foreach (var ingredient in recipe.Ingredients)
        {
            var isRecipe = _recipes.Any(r => r.Name.Equals(ingredient.Name, StringComparison.OrdinalIgnoreCase));
            if (isRecipe)
            {
                // Ingredient is a recipe -> Recursively process it
                await StartCookingIngredients(orderNumber, ingredient.Name, ingredientOfTree.Append(food).ToArray(),
                    id);
            }
            else
            {
                // Ingredient is a base ingredient -> ProcessTimerElapsedEvent will handle the rest
                var timerNote = new TimerNote(
                    orderNumber,
                    ingredient.Name,
                    ingredientOfTree.Append(food).ToArray(),
                    null,
                    null,
                    ingredient.Steps.ToList(),
                    _foodId
                );
                var timerNoteJson = JsonSerializer.Serialize(timerNote);
                var setTimerRequest = new SetTimerRequest(TimeSpan.FromMinutes(0), timerNoteJson);
                await _theCodeKitchenClient.SetTimer(setTimerRequest);
            }
        }
    }

    private async Task ProcessTimerElapsedEvent(TimerNote timerNote)
    {
        Console.WriteLine($"{Username} - Process Timer Elapsed Event - {JsonSerializer.Serialize(timerNote)}");

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
                await _theCodeKitchenClient.TakeFoodFromEquipment(
                    sourceEquipmentType, sourceEquipmentNumber.Value);
                await LockOrUnlockEquipment(sourceEquipmentType, sourceEquipmentNumber.Value, false);
            }
            else
            {
                // Food is in pantry -> Take from there
                await _theCodeKitchenClient.TakeFoodFromPantry(timerNote.Food);
            }

            // Add food to destination equipment, set timer for next step, update note for next step
            await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);

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

        // All steps are done and the food is not part of a recipe -> Notify head chef that food is ready
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

        // All steps are done, but is part of recipe -> Find the recipe and merge it if all ingredients are ready
        var recipeName = timerNote.RecipeTree.Last();
        var recipe = _recipes.First(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        var orderRecipeKey = $"{timerNote.Id}_{string.Join('_', timerNote.RecipeTree)}".ToUpper();

        var ingredientsToBePrepared = recipe.Ingredients.Where(i => i.Steps.Count > 0).ToList();
        var ingredientsToBeTakenFromPantry = recipe.Ingredients.Where(i =>
                i.Steps.Count <= 0 && _ingredients.Select(pi => pi.Name)
                    .Contains(i.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();

        var preparedIngredients = _preparedIngredientLocations
            .Where(kv => kv.Key.StartsWith(orderRecipeKey))
            .ToDictionary();

        var isToBePrepared = ingredientsToBePrepared.Select(i => i.Name)
            .Contains(timerNote.Food, StringComparer.OrdinalIgnoreCase);

        if ((!isToBePrepared && ingredientsToBePrepared.Count == preparedIngredients.Count) ||
            (isToBePrepared && ingredientsToBePrepared.Count == preparedIngredients.Count + 1))
        {
            // All ingredients with steps are ready, take the prepared ingredients, take the ingredients without steps from pantry and merge into recipe
            var firstRecipeStep = recipe.Steps.FirstOrDefault();
            var destinationEquipmentType = firstRecipeStep?.EquipmentType ?? EquipmentType.Counter;
            var destinationEquipmentNumber = FindAvailableEquipment(destinationEquipmentType);

            if (destinationEquipmentType == EquipmentType.Counter && destinationEquipmentNumber <= 0)
            {
                destinationEquipmentType = EquipmentType.HotPlate;
                destinationEquipmentNumber = FindAvailableEquipment(destinationEquipmentType);
            }

            if (destinationEquipmentNumber <= 0)
            {
                // No Counter or Hot Plate available -> Try again in 2 minutes
                await RetryLater(timerNote);
                return;
            }

            // Lock destination equipment
            await LockOrUnlockEquipment(destinationEquipmentType, destinationEquipmentNumber, true);

            if (isToBePrepared)
            {
                await _theCodeKitchenClient.TakeFoodFromEquipment(sourceEquipmentType!, sourceEquipmentNumber!.Value);
                await LockOrUnlockEquipment(sourceEquipmentType!, sourceEquipmentNumber.Value, false);
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
            }

            foreach (var preparedIngredient in preparedIngredients)
            {
                var location = preparedIngredient.Value;
                await _theCodeKitchenClient.TakeFoodFromEquipment(location.Item1, location.Item2);
                await LockOrUnlockEquipment(location.Item1, location.Item2, false);
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
                _preparedIngredientLocations.TryRemove(preparedIngredient.Key, out _);
            }

            foreach (var ingredient in ingredientsToBeTakenFromPantry)
            {
                await _theCodeKitchenClient.TakeFoodFromPantry(ingredient.Name);
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
            }

            var timerNoteForNextStep = new TimerNote(
                timerNote.Order,
                recipe.Name,
                timerNote.RecipeTree.Take(timerNote.RecipeTree.Length - 1).ToArray(),
                destinationEquipmentType,
                destinationEquipmentNumber,
                recipe.Steps.ToList(),
                timerNote.Id
            );
            var timerNoteForNextStepJson = JsonSerializer.Serialize(timerNoteForNextStep);
            var time = firstRecipeStep?.Time ?? TimeSpan.FromMinutes(0);
            var setTimerRequest = new SetTimerRequest(time, timerNoteForNextStepJson);
            await _theCodeKitchenClient.SetTimer(setTimerRequest);
        }
        else if (ingredientsToBePrepared.Select(i => i.Name).Contains(timerNote.Food, StringComparer.OrdinalIgnoreCase))
        {
            // Current ingredient is one of the ingredients with steps, move it onto counter or hot plate and track the prepared ingredient
            var destinationEquipmentType = EquipmentType.Counter;
            var destinationEquipmentNumber = FindAvailableEquipment(destinationEquipmentType);

            if (destinationEquipmentNumber <= 0)
            {
                destinationEquipmentType = EquipmentType.HotPlate;
                destinationEquipmentNumber = FindAvailableEquipment(destinationEquipmentType);
            }

            if (destinationEquipmentNumber <= 0)
            {
                // No Counter or Hot Plate available -> Try again in 2 minutes
                await RetryLater(timerNote);
                return;
            }

            // Lock destination equipment, take food, unlock source equipment, add food to destination equipment
            await LockOrUnlockEquipment(destinationEquipmentType, destinationEquipmentNumber, true);
            await _theCodeKitchenClient.TakeFoodFromEquipment(sourceEquipmentType!, sourceEquipmentNumber!.Value);
            await LockOrUnlockEquipment(sourceEquipmentType!, sourceEquipmentNumber.Value, false);
            await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);

            // Track the prepared ingredient
            var location = new Tuple<string, int>(destinationEquipmentType, destinationEquipmentNumber);
            var orderRecipeIngredientKey = $"{orderRecipeKey}_{timerNote.Food}".ToUpper();
            _preparedIngredientLocations[orderRecipeIngredientKey] = location;
        }
    }
}