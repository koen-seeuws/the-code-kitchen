using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Models.Order;
using TheCodeKitchen.Application.Contracts.Requests.Order;
using TheCodeKitchen.Application.Contracts.Response.Order;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain
{
    public async Task<Result<GenerateOrderResponse>> GenerateOrder(GenerateOrderRequest request)
    {
        var orderNumber = this.GetPrimaryKeyLong();
        var game = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The order with number {orderNumber} and game {game} has already been generated/initialized");
        
        logger.LogInformation("Game {gameId}: Generating order {number}...", game, orderNumber);
        
        var cookBookGrain = GrainFactory.GetGrain<ICookBookGrain>(Guid.Empty);
        
        var getRecipesResult = await cookBookGrain.GetRecipes();
        if (!getRecipesResult.Succeeded)
            return getRecipesResult.Error;
        var recipes = getRecipesResult.Value.ToArray();
        
        if (recipes.Length == 0)
            return new EmptyError("There are no recipes available to generate an order");

        var amountOfDishes = Random.Shared.Next(request.MinimumItemsPerOrder, request.MaximumItemsPerOrder);
        var foodRequests = Random.Shared
            .GetItems(recipes, amountOfDishes)
            .Select(r =>
            {
                var timeToPrepare = r.GetMinimumPreparationTime(recipes);
                return new OrderFoodRequest(r.Name, timeToPrepare);
            })
            .ToList();
        
        var minimumPreparationTime = foodRequests
            .Select(f => f.MinimumTimeToPrepareFood)
            .DefaultIfEmpty(TimeSpan.Zero)
            .Max();
        
        var order = new Order(orderNumber, game, foodRequests);
        state.State = order;
        await state.WriteStateAsync();

        var foodRequestDtos = mapper.Map<List<FoodRequestDto>>(foodRequests);
        var newOrderEvent = new NewOrderEvent(orderNumber, foodRequestDtos);
        
        var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Game);
        await stream.OnNextAsync(newOrderEvent);

        return new GenerateOrderResponse(minimumPreparationTime);
    }
}