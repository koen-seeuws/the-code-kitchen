namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record AddStepRequest(string EquipmentType, TimeSpan Time);