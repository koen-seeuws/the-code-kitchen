using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Response.Food;

[GenerateSerializer]
public record GetFoodResponse(Guid Id, string Name, double Temperature, ICollection<FoodDto> Ingredients, ICollection<RecipeStepDto> Steps, Guid Game, Guid Kitchen, string? Cook, string? CurrentEquipmentType, int? CurrentEquipmentNumber, long? OrderNumber);