using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record CreateFoodRequest(string Name, double Temperature, Guid Kitchen, FoodDto[]? Foods = null);