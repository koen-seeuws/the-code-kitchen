namespace TheCodeKitchen.Application.Contracts.Response;

public record GetKitchenResponse(Guid Id, string Name, string? Code);