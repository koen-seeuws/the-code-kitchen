namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record SetOrderRequest(string Cook, long OrderNumber);