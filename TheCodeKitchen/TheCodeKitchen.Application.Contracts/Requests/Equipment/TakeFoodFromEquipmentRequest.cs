namespace TheCodeKitchen.Application.Contracts.Requests.Equipment;

[GenerateSerializer]
public record TakeFoodFromEquipmentRequest(Guid Cook);