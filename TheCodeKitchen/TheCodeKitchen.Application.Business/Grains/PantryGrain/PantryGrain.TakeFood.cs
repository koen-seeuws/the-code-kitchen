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
        
        var getKitchenResult = await cookGrain.GetKitchen();

        if (!getKitchenResult.Succeeded)
            return getKitchenResult.Error;
        
        var foodId = Guid.CreateVersion7();
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(foodId);
        var createFoodRequest = new CreateFoodRequest(ingredient.Name, state.State.Temperature, getKitchenResult.Value.Game);
        var createFoodResult = await foodGrain.Initialize(createFoodRequest);

        if (!createFoodResult.Succeeded)
            return createFoodResult.Error;
        
        var holdFoodRequest = new HoldFoodRequest(foodId);
        var holdFoodResult = await cookGrain.HoldFood(holdFoodRequest);

        if (!holdFoodResult.Succeeded)
        {
            await foodGrain.Trash(); // Try to clean up if holding food fails
            return holdFoodResult.Error;
        }

        return new TakeFoodResponse(foodId);
    }
}