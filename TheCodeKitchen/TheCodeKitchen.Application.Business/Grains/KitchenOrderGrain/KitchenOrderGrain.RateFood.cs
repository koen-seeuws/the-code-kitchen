using TheCodeKitchen.Application.Contracts.Models;
using TheCodeKitchen.Application.Contracts.Response.CookBook;
using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    private double RateFood(string food, ICollection<RecipeStepDto> executedSteps, ICollection<FoodDto> ingredients,  ICollection<GetRecipeResponse> recipes)
    {
        var recipe = recipes.First(r => r.Name.Equals(food, StringComparison.OrdinalIgnoreCase));
        var rating = RateSteps(executedSteps, recipe.Steps);
        var ratings = ingredients
            .Select(f => RateFood(f.Name, f.Steps, f.Ingredients, recipes))
            .ToList();
        ratings.Add(rating);
        return ratings.Average();
    }

    private double RateSteps(ICollection<RecipeStepDto> executedSteps, ICollection<RecipeStepDto> expectedSteps)
    {
        //TODO: rate steps
        return 100.0;
    }
}