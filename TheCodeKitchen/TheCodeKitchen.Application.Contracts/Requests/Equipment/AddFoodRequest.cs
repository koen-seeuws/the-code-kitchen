namespace TheCodeKitchen.Application.Contracts.Requests.Equipment;

[GenerateSerializer]
public record AddFoodRequest(Guid Cook, Guid Food);