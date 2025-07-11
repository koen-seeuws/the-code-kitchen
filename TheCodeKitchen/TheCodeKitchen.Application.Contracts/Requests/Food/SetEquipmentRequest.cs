namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record SetEquipmentRequest(string EquipmentType, int EquipmentNumber);