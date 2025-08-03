using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Events;

public record NewKitchenOrderEvent(long Number, ICollection<FoodRequestDto> RequestedFoods);