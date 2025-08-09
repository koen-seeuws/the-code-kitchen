using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Events.Game;

[GenerateSerializer]
public record NewOrderEvent(long Number, ICollection<FoodRequestDto> RequestedFoods);