using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Equipment;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    public async Task<Result<TakeFoodResponse>> TakeFood(TakeFoodFromEquipmentRequest request)
    {
        if (state.State.Foods.Count <= 0)
            return new EquipmentEmptyError($"The equipment {this.GetPrimaryKey()} does not contain an item");

        if (state.State.Foods.Count > 1)
        {
            //Merge foods according to recipe
            var getFoodTasks = state.State.Foods.Select(async id =>
            {
                var foodGrain = GrainFactory.GetGrain<IFoodGrain>(id);
                var result = await foodGrain.GetFood();
                return result;
            });

            var results = await Task.WhenAll(getFoodTasks);

            var getFoodsResult = Result<GetFoodResponse>.Combine(results);

            if (!getFoodsResult.Succeeded)
                return getFoodsResult.Error;

            var foods = getFoodsResult.Value.ToList();

            var recipes = GrainFactory.GetGrain<ICookBookGrain>(Guid.Empty);
            var recipesResult = await recipes.GetRecipes();

            if (!recipesResult.Succeeded)
                return recipesResult.Error;

            var necessaryIngredientCombo = foods
                .Select(i => i.Name)
                .GetRecipeComboIdentifier();

            var recipe = recipesResult.Value
                .FirstOrDefault(r => r.Ingredients
                    .Select(i => i.Name)
                    .GetRecipeComboIdentifier()
                    .Equals(necessaryIngredientCombo, StringComparison.OrdinalIgnoreCase)
                );

            var foodId = Guid.CreateVersion7();
            var newFoodGrain = GrainFactory.GetGrain<IFoodGrain>(foodId);
            
            var createFoodRequest = new CreateFoodRequest(
                recipe?.Name ?? "UNKNOWN",
                foods.Select(f => f.Temperature).Average(),
                state.State.Kitchen,
                foods
                    .Select(f =>
                        new FoodDto(
                            f.Id,
                            f.Name,
                            f.Temperature,
                            f.Ingredients,
                            f.Steps,
                            f.Kitchen,
                            null,
                            null,
                            null,
                            null
                        )
                    )
                    .ToArray()
            );
            
            var createFoodResult = await newFoodGrain.Initialize(createFoodRequest);

            if (!createFoodResult.Succeeded)
                return createFoodResult.Error;

            var ingredients = state.State.Foods;
            state.State.Foods = [foodId];
            
            await state.WriteStateAsync(); // Intermediate state write

            foreach (var ingredient in ingredients)
            {
                // Fire and forget to trash the ingredients
                _ = Task.Run(async () =>
                {
                    var foodGrain = GrainFactory.GetGrain<IFoodGrain>(ingredient);
                    await foodGrain.Trash();
                });
            }
        }

        // TODO: Handling single/merged food item and/or steps from processing in this equipment
        
        var food = state.State.Foods.First();
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(request.Cook);
        var holdFoodRequest = new HoldFoodRequest(food);

        var holdFoodResult = await cookGrain.HoldFood(holdFoodRequest);
        
        if(!holdFoodResult.Succeeded)
            return holdFoodResult.Error;
        
        //TODO: Check if this can be improved (that this call becomes unnecessary)
        var foodGrain = GrainFactory.GetGrain<IFoodGrain>(state.State.Foods.First());
        var getFoodResult = await foodGrain.GetFood();
        if (!getFoodResult.Succeeded)
            return getFoodResult.Error;
        return mapper.Map<TakeFoodResponse>(getFoodResult.Value);
    }
}