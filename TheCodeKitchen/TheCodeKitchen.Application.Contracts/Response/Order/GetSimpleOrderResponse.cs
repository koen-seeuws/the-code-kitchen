namespace TheCodeKitchen.Application.Contracts.Response.Order;

[GenerateSerializer]
public record GetSimpleOrderResponse(long Number, ICollection<string> RequestedFoods);