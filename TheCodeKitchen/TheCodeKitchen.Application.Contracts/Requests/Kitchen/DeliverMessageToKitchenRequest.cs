namespace TheCodeKitchen.Application.Contracts.Requests.Kitchen;

public record DeliverMessageToKitchenRequest(string From, string? To, string Content);