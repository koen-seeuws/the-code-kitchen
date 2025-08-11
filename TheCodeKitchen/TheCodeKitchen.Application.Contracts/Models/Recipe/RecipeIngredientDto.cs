namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record RecipeIngredientDto(string Name, ICollection<RecipeStepDto> Steps);