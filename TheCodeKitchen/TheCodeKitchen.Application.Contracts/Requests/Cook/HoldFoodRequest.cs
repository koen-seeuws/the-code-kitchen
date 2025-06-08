namespace TheCodeKitchen.Application.Contracts.Requests.Cook;

[GenerateSerializer]
public record HoldFoodRequest(Guid FoodId);