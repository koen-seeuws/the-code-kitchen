namespace TheCodeKitchen.Application.Contracts.Requests.Kitchen;

public record SendMessageRequest(string? To, string Content);