namespace TheCodeKitchen.Application.Contracts.Response;

public record AddKitchenResponse(
    Guid Id,
    string Name,
    string Code
);