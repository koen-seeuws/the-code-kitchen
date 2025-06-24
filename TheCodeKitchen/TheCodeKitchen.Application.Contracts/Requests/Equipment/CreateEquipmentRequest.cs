namespace TheCodeKitchen.Application.Contracts.Requests.Equipment;

[GenerateSerializer]
public record CreateEquipmentRequest(Guid Kitchen, int Number);