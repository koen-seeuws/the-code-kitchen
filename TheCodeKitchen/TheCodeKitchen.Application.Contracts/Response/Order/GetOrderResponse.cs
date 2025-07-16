namespace TheCodeKitchen.Application.Contracts.Response.Order;

[GenerateSerializer]
public record GetOrderResponse(long Number, IDictionary<string, TimeSpan> RequestedFoodsWithTimeToComplete);