using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Business.Extensions;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Generate()
    {
        var orderNumber = this.GetPrimaryKeyLong();
        var game = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The order with number {orderNumber} and game {game} has already been generated/initialized");
        
        logger.LogInformation("Game {gameId}: Generating order {number}...", state.State.Game, orderNumber);
        
        var cookBookGrain = GrainFactory.GetGrain<ICookBookGrain>(Guid.Empty);
        
        var getRecipesResult = await cookBookGrain.GetRecipes();
        if (!getRecipesResult.Succeeded)
            return getRecipesResult.Error;
        var recipes = getRecipesResult.Value.ToArray();
        
        var amountOfDishes = Random.Shared.Next(1, 9);
        var dishesWithTimeToPrepare = Random.Shared
            .GetItems(recipes, amountOfDishes)
            .ToDictionary(
                r => r.Name,
                r => r.GetMinimumPreparationTime(recipes)
            );
        
        var order = new Order(orderNumber, game, dishesWithTimeToPrepare);
        state.State = order;
        await state.WriteStateAsync();
        
        var newOrderEvent = new NewOrderEvent(orderNumber, dishesWithTimeToPrepare);
        
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Game);
        await stream.OnNextAsync(newOrderEvent);

        return TheCodeKitchenUnit.Value;
    }
}