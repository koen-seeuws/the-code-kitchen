using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Channels;
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

    // Configuration
    private readonly string _headChef;
    private readonly string _equipmentCoordinator;

    // Cached Data
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();

    // Equipment Locking
    private TaskCompletionSource<MessageContent>? _pendingGrant;

    // Food Processing
    private int _foodId = 1;
    private readonly SemaphoreSlim _holdFoodSemaphore = new(1, 1);
    
    private readonly ConcurrentDictionary<string, bool> _inProgressFoods = new();
    
    
    

    private readonly ConcurrentDictionary<string, Tuple<string, int>> _preparedIngredientLocations =
        new(StringComparer.OrdinalIgnoreCase);

    public Chef(string headChef, string equipmentCoordinator, string kitchenCode, string username, string password,
        TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        _headChef = headChef;
        _equipmentCoordinator = equipmentCoordinator;
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

            var note = JsonSerializer.Deserialize<TimerNote>(timerElapsedEvent.Note!);

            try
            {
                await ProcessTimerElapsedEvent(note!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"{Username} - Error processing timer elapsed event {JsonSerializer.Serialize(timerElapsedEvent)}: {ex}");
                throw;
            }
        };

        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            var message = new Message(
                messageReceivedEvent.Number,
                messageReceivedEvent.From,
                messageReceivedEvent.To,
                JsonSerializer.Deserialize<MessageContent>(messageReceivedEvent.Content)!
            );

            try
            {
                await ProcessMessage(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"{Username} - Error processing message {JsonSerializer.Serialize(messageReceivedEvent)}: {ex}");
                throw;
            }
        };
    }

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        _recipes = await _theCodeKitchenClient.ReadRecipes(cancellationToken);
        _ingredients = await _theCodeKitchenClient.PantryInventory(cancellationToken);
    }

    private async Task ProcessMessage(Message message)
    {
        switch (message.Content.Code)
        {
            case MessageCodes.GrantEquipment:
            {
                Console.WriteLine(
                    $"{Username} - Process Message - Granted Equipment - {message.Content.EquipmentType} {message.Content.EquipmentNumber}");
                _pendingGrant?.SetResult(message.Content);
                break;
            }
            case MessageCodes.CookFood:
            {
                Console.WriteLine(
                    $"{Username} - Process Message - Cooking Food - Order: {message.Content.Order!.Value}, Food: {message.Content.Food!}");
                var foodId = Interlocked.Increment(ref _foodId);
                await StartCookingIngredients(message.Content.Order!.Value, message.Content.Food!, [], foodId);
                break;
            }
        }

        var confirmMessageRequest = new ConfirmMessageRequest(message.Number);
        await _theCodeKitchenClient.ConfirmMessage(confirmMessageRequest);
    }

    private async Task ReleaseEquipment(string equipmentType, int number)
    {
        Console.WriteLine($"{Username} - Releasing Equipment - {equipmentType} {number}");
        var release = new MessageContent(MessageCodes.ReleaseEquipment, null, null, equipmentType, number);
        await _theCodeKitchenClient.SendMessage(new SendMessageRequest(_equipmentCoordinator,
            JsonSerializer.Serialize(release)));
    }

    private async Task<int> RequestEquipment(string equipmentType)
    {
        _pendingGrant = new TaskCompletionSource<MessageContent>();

        var request = new MessageContent(MessageCodes.RequestEquipment, null, null, equipmentType, null);
        await _theCodeKitchenClient.SendMessage(new SendMessageRequest(_equipmentCoordinator,
            JsonSerializer.Serialize(request)));

        // Wait for GrantEquipment for this chef (wait until coordinator responds)
        var grant = await _pendingGrant.Task;

        _pendingGrant = null;

        return grant.EquipmentNumber!.Value;
    }

    private async Task SetTimer(long orderNumber, string food, string[] recipeTree, RecipeStepDto[] stepsToDo, int id,
        string? equipmentType, int? equipmentNumber, TimeSpan duration)
    {
        var note = new TimerNote(
            orderNumber,
            food,
            recipeTree,
            equipmentType,
            equipmentNumber,
            stepsToDo,
            id
        );
        var jsonNote = JsonSerializer.Serialize(note);
        var setTimerRequest = new SetTimerRequest(duration, jsonNote);
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
                // Ingredient is a base ingredient -> Cook the food
                await CookFood(orderNumber, ingredient.Name, ingredientOfTree.Append(food).ToArray(),
                    ingredient.Steps.ToArray(), id, null, null);
            }
        }
    }

    private async Task CookFood(long orderNumber, string food, string[] recipeTree, RecipeStepDto[] stepsToDo, int id,
        string? sourceEquipmentType, int? sourceEquipmentNumber)
    {
        var allStepsDone = !stepsToDo.Any();

        // Not all steps done -> Simply put it in the next necessary equipment
        if (!allStepsDone)
        {
            var stepToDo = stepsToDo.First();
            var destinationEquipmentType = stepToDo.EquipmentType;
            var destinationEquipmentNumber = await RequestEquipment(destinationEquipmentType);
            if (destinationEquipmentNumber < 0)
            {
                // No equipment available -> Try again in 2 minutes
                await SetTimer(orderNumber, food, recipeTree, stepsToDo, id, sourceEquipmentType, sourceEquipmentNumber,
                    TimeSpan.FromMinutes(2));
                return;
            }

            await _holdFoodSemaphore.WaitAsync();
            try
            {
                if (sourceEquipmentType != null && sourceEquipmentNumber.HasValue)
                {
                    // Food is in equipment -> Take from there
                    await _theCodeKitchenClient.TakeFoodFromEquipment(sourceEquipmentType, sourceEquipmentNumber.Value);
                    await ReleaseEquipment(sourceEquipmentType, sourceEquipmentNumber.Value);
                }
                else
                {
                    // Food is in pantry -> Take from there
                    await _theCodeKitchenClient.TakeFoodFromPantry(food);
                }

                // Add food to destination equipment, set timer for next step, update note for next step
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
            }
            finally
            {
                _holdFoodSemaphore.Release();
            }

            await SetTimer(
                orderNumber,
                food,
                recipeTree,
                stepsToDo.Skip(1).ToArray(),
                id,
                destinationEquipmentType,
                destinationEquipmentNumber,
                stepToDo.Time
            );
            return;
        }

        // All steps are done and the food is not part of a recipe -> Notify head chef that food is ready
        var isRequestedByHeadChef = recipeTree.Length <= 0;
        if (isRequestedByHeadChef)
        {
            var messageContent = new MessageContent(
                MessageCodes.FoodReady,
                orderNumber,
                food,
                sourceEquipmentType,
                sourceEquipmentNumber
            );
            var sendMessage = new SendMessageRequest(_headChef, JsonSerializer.Serialize(messageContent));
            await _theCodeKitchenClient.SendMessage(sendMessage);
            return;
        }

        // All steps are done, but is part of recipe -> Find the recipe and merge it if all ingredients are ready
        var recipeName = recipeTree.Last();
        var recipe = _recipes.First(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        var recipeKey = $"{id}_{string.Join('_', recipeTree)}".ToUpper();

        var ingredientsToBePrepared = recipe.Ingredients.Where(i => i.Steps.Count > 0).ToList();
        var ingredientsToBeTakenFromPantry = recipe.Ingredients.Where(i =>
                i.Steps.Count <= 0 && _ingredients.Select(pi => pi.Name)
                    .Contains(i.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();

        var preparedIngredients = _preparedIngredientLocations
            .Where(kv => kv.Key.StartsWith(recipeKey))
            .ToDictionary();

        var isToBePrepared = ingredientsToBePrepared
            .Select(i => i.Name)
            .Contains(food, StringComparer.OrdinalIgnoreCase);

        if ((!isToBePrepared && ingredientsToBePrepared.Count == preparedIngredients.Count) ||
            (isToBePrepared && ingredientsToBePrepared.Count == preparedIngredients.Count + 1))
        {
            // All ingredients with steps are ready, take the prepared ingredients, take the ingredients without steps from pantry and merge into recipe
            var firstRecipeStep = recipe.Steps.FirstOrDefault();
            var destinationEquipmentType = firstRecipeStep?.EquipmentType ?? EquipmentType.Counter;
            var destinationEquipmentNumber = await RequestEquipment(destinationEquipmentType);

            if (destinationEquipmentType == EquipmentType.Counter && destinationEquipmentNumber < 0)
            {
                destinationEquipmentType = EquipmentType.HotPlate;
                destinationEquipmentNumber = await RequestEquipment(destinationEquipmentType);
            }

            if (destinationEquipmentNumber < 0)
            {
                // No Counter or Hot Plate available -> Try again in 2 minutes
                await SetTimer(
                    orderNumber,
                    food,
                    recipeTree,
                    stepsToDo,
                    id,
                    sourceEquipmentType,
                    sourceEquipmentNumber,
                    TimeSpan.FromMinutes(2)
                );
                return;
            }

            await _holdFoodSemaphore.WaitAsync();
            try
            {
                if (isToBePrepared)
                {
                    await _theCodeKitchenClient.TakeFoodFromEquipment(sourceEquipmentType!,
                        sourceEquipmentNumber!.Value);
                    await ReleaseEquipment(sourceEquipmentType!, sourceEquipmentNumber.Value);
                    await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType,
                        destinationEquipmentNumber);
                }

                foreach (var preparedIngredient in preparedIngredients)
                {
                    var location = preparedIngredient.Value;
                    await _theCodeKitchenClient.TakeFoodFromEquipment(location.Item1, location.Item2);
                    await ReleaseEquipment(location.Item1, location.Item2);
                    await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType,
                        destinationEquipmentNumber);
                    _preparedIngredientLocations.TryRemove(preparedIngredient.Key, out _);
                }

                foreach (var ingredient in ingredientsToBeTakenFromPantry)
                {
                    await _theCodeKitchenClient.TakeFoodFromPantry(ingredient.Name);
                    await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType,
                        destinationEquipmentNumber);
                }
            }
            finally
            {
                _holdFoodSemaphore.Release();
            }

            //Why this?
            if (firstRecipeStep != null)
            {
                await SetTimer(
                    orderNumber,
                    recipe.Name,
                    recipeTree.SkipLast(1).ToArray(),
                    recipe.Steps.Skip(1).ToArray(),
                    id,
                    destinationEquipmentType,
                    destinationEquipmentNumber,
                    firstRecipeStep.Time
                );
            }
            else
            {
                await CookFood(
                    orderNumber,
                    recipe.Name,
                    recipeTree.SkipLast(1).ToArray(),
                    recipe.Steps.Skip(1).ToArray(),
                    id,
                    destinationEquipmentType,
                    destinationEquipmentNumber
                );
            }
        }
        else if (ingredientsToBePrepared.Select(i => i.Name).Contains(food, StringComparer.OrdinalIgnoreCase))
        {
            // Current ingredient is one of the ingredients with steps, move it onto counter or hot plate and track the prepared ingredient
            var destinationEquipmentType = EquipmentType.Counter;
            var destinationEquipmentNumber = await RequestEquipment(destinationEquipmentType);

            if (destinationEquipmentNumber < 0)
            {
                destinationEquipmentType = EquipmentType.HotPlate;
                destinationEquipmentNumber = await RequestEquipment(destinationEquipmentType);
            }

            if (destinationEquipmentNumber < 0)
            {
                // No Counter or Hot Plate available -> Try again in 2 minutes
                await SetTimer(orderNumber, food, recipeTree, stepsToDo, id, sourceEquipmentType, sourceEquipmentNumber,
                    TimeSpan.FromMinutes(2));
                return;
            }

            // Lock destination equipment, take food, unlock source equipment, add food to destination equipment
            await _holdFoodSemaphore.WaitAsync();
            try
            {
                await _theCodeKitchenClient.TakeFoodFromEquipment(sourceEquipmentType!, sourceEquipmentNumber!.Value);
                await ReleaseEquipment(sourceEquipmentType!, sourceEquipmentNumber.Value);
                await _theCodeKitchenClient.AddFoodToEquipment(destinationEquipmentType, destinationEquipmentNumber);
            }
            finally
            {
                _holdFoodSemaphore.Release();
            }

            // Track the prepared ingredient
            var location = new Tuple<string, int>(destinationEquipmentType, destinationEquipmentNumber);
            var orderRecipeIngredientKey = $"{recipeKey}_{food}".ToUpper();
            _preparedIngredientLocations[orderRecipeIngredientKey] = location;
        }
    }

    private async Task ProcessTimerElapsedEvent(TimerNote timerNote)
    {
        Console.WriteLine($"{Username} - Process Timer Elapsed Event - {JsonSerializer.Serialize(timerNote)}");
        await CookFood(timerNote.Order, timerNote.Food, timerNote.RecipeTree, timerNote.StepsToDo, timerNote.Id,
            timerNote.EquipmentType, timerNote.EquipmentNumber);
    }
}