namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record RecipeIngredientDto(string Name, RecipeStepDto[] Steps);