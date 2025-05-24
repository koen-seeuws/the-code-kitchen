using System.Security.AccessControl;

namespace TheCodeKitchen.Application.Contracts.Requests.Equipment;

[GenerateSerializer]
public record CreateEquipmentRequest(Guid Game, Guid Kitchen, int Number);