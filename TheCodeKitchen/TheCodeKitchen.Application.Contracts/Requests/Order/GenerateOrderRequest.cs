namespace TheCodeKitchen.Application.Contracts.Requests.Order;

public record GenerateOrderRequest(short MinimumItemsPerOrder, short MaximumItemsPerOrder);