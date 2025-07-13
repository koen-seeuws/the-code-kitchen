using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Requests.Food;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    private async Task<Result<TheCodeKitchenUnit>> MixFood()
    {
        if (!state.RecordExists)
        {
            var kitchen = this.GetPrimaryKey();
            var primaryKeyExtensions = this.GetPrimaryKeyString().Split('+');
            var equipmentType = primaryKeyExtensions[1];
            var number = int.Parse(primaryKeyExtensions[2]);

            return new AlreadyExistsError(
                $"The equipment {equipmentType} {number} does not exist in kitchen {kitchen}");
        }
        
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
            recipe?.Name ?? "UNKNOWN MIXTURE",
            foods.Select(f => f.Temperature).Average(),
            state.State.Game,
            state.State.Kitchen,
            foods
                .Select(f =>
                    new FoodDto(
                        f.Id,
                        f.Name,
                        f.Temperature,
                        f.Ingredients,
                        f.Steps,
                        f.Game,
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

        await state.WriteStateAsync();

        foreach (var ingredient in ingredients)
        {
            // Fire and forget to trash the ingredients
            _ = Task.Run(async () =>
            {
                var foodGrain = GrainFactory.GetGrain<IFoodGrain>(ingredient);
                await foodGrain.Trash();
            });
        }

        return TheCodeKitchenUnit.Value;
    }
}