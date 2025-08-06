using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record NewKitchenOrderEvent(long Number, ICollection<FoodRequestDto> RequestedFoods);