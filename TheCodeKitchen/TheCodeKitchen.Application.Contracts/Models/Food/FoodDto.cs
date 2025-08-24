using TheCodeKitchen.Application.Contracts.Models.Recipe;

namespace TheCodeKitchen.Application.Contracts.Models.Food;

[GenerateSerializer]
public record FoodDto(
    Guid Id,
    string Name,
    double Temperature,
    ICollection<FoodDto> Ingredients,
    ICollection<RecipeStepDto> Steps,
    Guid Game,
    Guid Kitchen,
    string? Cook,
    string? CurrentEquipmentType,
    int? CurrentEquipmentNumber,
    long? OrderNumber
);