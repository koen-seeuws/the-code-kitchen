using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record SetEquipmentRequest(EquipmentType EquipmentType, int EquipmentNumber);