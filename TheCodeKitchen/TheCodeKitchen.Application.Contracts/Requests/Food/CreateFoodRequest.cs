using TheCodeKitchen.Application.Contracts.Models.Food;

namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record CreateFoodRequest(string Name, double Temperature, Guid Game, Guid Kitchen, ICollection<FoodDto>? Foods = null);