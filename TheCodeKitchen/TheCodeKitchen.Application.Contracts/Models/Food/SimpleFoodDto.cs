using TheCodeKitchen.Application.Contracts.Models.Recipe;

namespace TheCodeKitchen.Application.Contracts.Models.Food;

[GenerateSerializer]
public record SimpleFoodDto(string Name, double Temperature, ICollection<RecipeStepDto> Steps);