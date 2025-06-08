namespace TheCodeKitchen.Application.Contracts.Requests.Kitchen;

[GenerateSerializer]
public record CreateKitchenOrderRequest(Guid KitchenId, long OrderNumber);