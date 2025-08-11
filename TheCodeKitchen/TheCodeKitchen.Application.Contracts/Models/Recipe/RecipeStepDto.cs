namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record RecipeStepDto(string EquipmentType, TimeSpan Time);