using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Requests.CookBook;

[GenerateSerializer]
public record CreateRecipeRequest(string Name, ICollection<RecipeStepDto> Steps, ICollection<RecipeIngredientDto> Ingredients);