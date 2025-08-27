using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;
using TheCodeKitchen.Application.Contracts.Response.CookBook;

namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public sealed partial class CookBookGrain
{
    public async Task<Result<CreateRecipeResponse>> CreateRecipe(CreateRecipeRequest request)
    {
        logger.LogInformation("CookBook {CookBook}: Creating new recipe {newRecipeName}...", state.State.Id,
            request.Name);

        var newRecipeName = request.Name.Trim().ToCamelCase();

        // Check if recipe already exists
        var availableRecipes = state.State.Recipes.Select(r => r.Name).ToList();

        if (availableRecipes.Any(i => i.Equals(newRecipeName, StringComparison.OrdinalIgnoreCase)))
        {
            logger.LogWarning("CookBook {CookBook}: The recipe {newRecipeName} already exists", state.State.Id,
                newRecipeName);
            return new AlreadyExistsError($"The recipe {newRecipeName} already exists");
        }


        // Check if name is already used for an ingredient in pantry
        var pantryGrain = GrainFactory.GetGrain<IPantryGrain>(state.State.Id);
        var pantryIngredientsResult = await pantryGrain.GetIngredients();

        if (!pantryIngredientsResult.Succeeded)
        {
            return pantryIngredientsResult.Error;
        }

        var availableIngredients = pantryIngredientsResult.Value.Select(i => i.Name).ToList();

        if (availableIngredients.Any(i => i.Equals(newRecipeName, StringComparison.OrdinalIgnoreCase)))
        {
            logger.LogWarning(
                "CookBook {CookBook}: {newRecipeName} already exists as an ingredient in the pantry",
                state.State.Id, newRecipeName);
            return new AlreadyExistsError($"{newRecipeName} already exists as an ingredient in the pantry");
        }

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
                logger.LogWarning(
                    "CookBook {CookBook}: The recipe {newRecipeName} cannot be created because this combination of ingredients is alreadyused for recipe {recipeName}",
                    state.State.Id, newRecipeName, recipe.Name);
                return new AlreadyExistsError(
                    $"This combination of ingredients is already used for recipe {recipe.Name}");
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
            if (!isRecipe && !isIngredient)
                return new InvalidRecipeError(
                    $"The ingredient {necessaryIngredientName} is not available in the pantry or as a recipe");
            

            if (isRecipe && necessaryIngredient.Steps.Count != 0)
                return new InvalidRecipeError("The subrecipes in a new recipe should not contain any steps");
            
            var necessaryIngredientSteps = necessaryIngredient.Steps
                .Select(i =>
                {
                    var equipmentType = i.EquipmentType.Trim().ToCamelCase();
                    return new RecipeStep(equipmentType, i.Time);
                })
                .ToList();

            var newRecipeIngredient = new RecipeIngredient(necessaryIngredientName, necessaryIngredientSteps);
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        var newRecipeSteps = request.Steps
            .Select(i =>
            {
                var equipmentType = i.EquipmentType.Trim().ToCamelCase();
                return new RecipeStep(equipmentType, i.Time);
            })
            .ToList();

        var newRecipe = new Recipe(newRecipeName, newRecipeIngredients, newRecipeSteps);

        state.State.Recipes.Add(newRecipe);
        await state.WriteStateAsync();
        
        return mapper.Map<CreateRecipeResponse>(newRecipe);
    }
}