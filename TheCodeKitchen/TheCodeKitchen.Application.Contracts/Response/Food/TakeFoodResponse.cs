using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Response.Food;

[GenerateSerializer]
public record TakeFoodResponse(string Name, double Temperature, ICollection<RecipeStepDto> Steps);