namespace TheCodeKitchen.Application.Contracts.Response;

public record CreateKitchenResponse(
    Guid Id,
    string Name,
    string Code
);