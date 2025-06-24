namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record SetOrderRequest(Guid Cook, long OrderNumber);