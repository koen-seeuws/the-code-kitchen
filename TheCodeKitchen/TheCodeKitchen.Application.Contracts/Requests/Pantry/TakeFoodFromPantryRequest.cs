namespace TheCodeKitchen.Application.Contracts.Requests.Pantry;

public record TakeFoodFromPantryRequest(string Ingredient, Guid Cook);