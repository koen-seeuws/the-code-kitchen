namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record FoodDto(
    Guid Id,
    string Name,
    double Temperature,
    ICollection<FoodDto> Ingredients,
    ICollection<RecipeStepDto> Steps,
    Guid Game,
    Guid Kitchen,
    Guid? Cook,
    string? CurrentEquipmentType,
    int? CurrentEquipmentNumber,
    long? OrderNumber
);