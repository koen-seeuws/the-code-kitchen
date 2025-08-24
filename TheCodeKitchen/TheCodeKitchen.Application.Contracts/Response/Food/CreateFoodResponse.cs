using TheCodeKitchen.Application.Contracts.Models.Food;
using TheCodeKitchen.Application.Contracts.Models.Recipe;

namespace TheCodeKitchen.Application.Contracts.Response.Food;

[GenerateSerializer]
public record CreateFoodResponse(Guid Id, string Name, double Temperature, ICollection<FoodDto>? Ingredients, ICollection<RecipeStepDto> Steps, Guid Kitchen, string? Cook, string? CurrentEquipmentType, int? CurrentEquipmentNumber, long? OrderNumber);