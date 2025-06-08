using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.PantryGrain;

public partial class PantryGrain
{
    public async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromPantryRequest request)
    {
        var ingredient = state.State.Ingredients.FirstOrDefault(i =>
            string.Equals(i.Name, request.Ingredient, StringComparison.InvariantCultureIgnoreCase));

        if (ingredient == null)
            return new NotFoundError($"The ingredient with name {request.Ingredient} was not found in the pantry");
        
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var cookCurrentFood = await cookGrain.CurrentFood();

        if (!cookCurrentFood.Succeeded)
            return cookCurrentFood.Error;

        if (cookCurrentFood.Value.FoodId != null)
            return new AlreadyHoldingFoodError($"You are already holding food with id {cookCurrentFood.Value.FoodId}");
        
        var foodId = Guid.CreateVersion7();
        var createFoodRequest = new CreateFoodRequest(ingredient.Name, state.State.Temperature);
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(foodId);
        var newFood = await foodGrain.Initialize(createFoodRequest);
        
        if(!newFood.Succeeded)
            return newFood.Error;
        
        var holdFoodRequest = new HoldFoodRequest(foodId);
        await cookGrain.HoldFood(holdFoodRequest);
        
        if(!newFood.Succeeded)
            return newFood.Error;

        return new TakeFoodResponse(foodId);
    }
}