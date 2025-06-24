namespace TheCodeKitchen.Application.Contracts.Requests.KitchenOrder;

[GenerateSerializer]
public record CreateKitchenOrderRequest(Guid KitchenId, long OrderNumber);