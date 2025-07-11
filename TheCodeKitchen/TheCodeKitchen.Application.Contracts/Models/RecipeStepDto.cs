using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record RecipeStepDto(EquipmentType EquipmentType, TimeSpan Time);