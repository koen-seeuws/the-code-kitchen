using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record CreateFoodRequest(string Name, double Temperature, Guid Game, Guid Kitchen, ICollection<FoodDto>? Foods = null);