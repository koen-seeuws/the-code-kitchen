namespace TheCodeKitchen.Application.Contracts.Response.Food;

//TODO: remove FoodId ans add Food Fields
[GenerateSerializer]
public record TakeFoodResponse(Guid FoodId);