namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record CreateKitchenRequest(string? Name, Guid GameId);