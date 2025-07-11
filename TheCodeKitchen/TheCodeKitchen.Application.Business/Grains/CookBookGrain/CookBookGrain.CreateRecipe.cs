using System.Net.Sockets;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;

namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public partial class CookBookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> CreateRecipe(CreateRecipeRequest request)
    {
        var newRecipeName = request.Name.Trim().ToCamelCase();
        
        // Check if recipe already exists
        var availableRecipes = state.State.Recipes.Select(r => r.Name).ToList();

        if (availableRecipes.Any(i => i.Equals(newRecipeName, StringComparison.OrdinalIgnoreCase)))
            return new AlreadyExistsError($"The recipe {newRecipeName} already exists");

        // Check if name is already used for an ingredient in pantry
        var pantryGrain = GrainFactory.GetGrain<IPantryGrain>(state.State.Id);
        var pantryIngredientsResult = await pantryGrain.GetIngredients();

        if (!pantryIngredientsResult.Succeeded)
        {
            return pantryIngredientsResult.Error;
        }
        
        var availableIngredients = pantryIngredientsResult.Value.Select(i => i.Name).ToList();

        if (availableIngredients.Any(i => i.Equals(newRecipeName, StringComparison.OrdinalIgnoreCase)))
            return new AlreadyExistsError($"{newRecipeName} already exists as an ingredient in the pantry");
        
        // Check if recipe ingredients combination is unique
        var newIngredientCombo = request.Ingredients
            .Select(i => i.Name)
            .GetRecipeComboIdentifier();

        foreach (var recipe in state.State.Recipes)
        {
            var ingredientCombo = recipe.Ingredients
                .Select(i => i.Name)
                .GetRecipeComboIdentifier();
            
            if (ingredientCombo.Equals(newIngredientCombo, StringComparison.OrdinalIgnoreCase))
            {
                return new AlreadyExistsError($"This combination of ingredients is already used for recipe {recipe.Name}");
            }
        }
        
        // Start crafting recipe
        var newRecipeIngredients = new List<RecipeIngredient>();
        
        foreach (var necessaryIngredient in request.Ingredients)
        {
            var necessaryIngredientName = necessaryIngredient.Name.Trim().ToCamelCase();
            
            var isRecipe = availableRecipes.Contains(necessaryIngredientName);
            var isIngredient = availableIngredients.Contains(necessaryIngredientName);
            
            // Check if necessary ingredient is available in pantry or as a recipe
            if(!isRecipe && !isIngredient)
            {
                return new NotFoundError($"The ingredient {necessaryIngredientName} is not available in the pantry or as a recipe");
            }

            if (isRecipe && necessaryIngredient.Steps.Any())
            {
                // TODO: not sure if sub recipe should be allowed to have extra steps
            }
            
            var necessaryIngredientSteps = mapper.Map<List<RecipeStep>>(necessaryIngredient.Steps);
            var newRecipeIngredient = new RecipeIngredient(necessaryIngredientName, necessaryIngredientSteps);
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        var newRecipeSteps = mapper.Map<List<RecipeStep>>(request.Steps);
        
        var newRecipe = new Recipe(newRecipeName, newRecipeIngredients, newRecipeSteps);
        
        state.State.Recipes.Add(newRecipe);
        await state.WriteStateAsync();
        
        return TheCodeKitchenUnit.Value;
    }
}