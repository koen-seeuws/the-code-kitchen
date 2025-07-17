using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Models;

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
        var foodRequests = Random.Shared
            .GetItems(recipes, amountOfDishes)
            .Select(r =>
            {
                var timeToPrepare = r.GetMinimumPreparationTime(recipes);
                return new FoodRequest(r.Name, timeToPrepare);
            })
            .ToList();
        
        var order = new Order(orderNumber, game, foodRequests);
        state.State = order;
        await state.WriteStateAsync();

        var foodRequestDtos = mapper.Map<List<FoodRequestDto>>(foodRequests);
        var newOrderEvent = new NewOrderEvent(orderNumber, foodRequestDtos);
        
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Game);
        await stream.OnNextAsync(newOrderEvent);

        return TheCodeKitchenUnit.Value;
    }
}