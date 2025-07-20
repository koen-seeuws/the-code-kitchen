using TheCodeKitchen.Application.Contracts.Models;

namespace TheCodeKitchen.Application.Contracts.Response.Order;

[GenerateSerializer]
public record GetOrderResponse(long Number, ICollection<FoodRequestDto> RequestedFoods);