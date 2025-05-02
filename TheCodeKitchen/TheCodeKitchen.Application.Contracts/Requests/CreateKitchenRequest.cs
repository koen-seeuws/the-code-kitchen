namespace TheCodeKitchen.Application.Contracts.Requests;

public record CreateKitchenRequest(string? Name, Guid GameId);