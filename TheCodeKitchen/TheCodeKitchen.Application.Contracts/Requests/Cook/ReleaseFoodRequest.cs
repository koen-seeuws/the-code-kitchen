namespace TheCodeKitchen.Application.Contracts.Requests.Cook;

[GenerateSerializer]
public record ReleaseFoodRequest(Guid FoodId);