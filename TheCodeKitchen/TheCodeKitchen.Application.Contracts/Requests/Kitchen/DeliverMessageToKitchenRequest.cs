namespace TheCodeKitchen.Application.Contracts.Requests.Kitchen;

[GenerateSerializer]
public record DeliverMessageToKitchenRequest(string From, string? To, string Content);