namespace TheCodeKitchen.Application.Contracts.Response.Food;

[GenerateSerializer]
public record CreateFoodResponse(Guid Id, string Name, string Temperature);