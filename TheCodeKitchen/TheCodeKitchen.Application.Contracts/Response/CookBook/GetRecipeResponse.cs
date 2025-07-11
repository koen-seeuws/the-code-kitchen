using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Response.CookBook;

[GenerateSerializer]
public record GetRecipeResponse(string Name, IEnumerable<RecipeStepDto> Steps, IEnumerable<RecipeIngredientDto> Ingredients);