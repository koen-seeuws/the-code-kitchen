namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record SimpleFoodDto(string Name, double Temperature, ICollection<RecipeStepDto> Steps);