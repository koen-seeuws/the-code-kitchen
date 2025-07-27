namespace TheCodeKitchen.Application.Contracts.Response.Kitchen;

[GenerateSerializer]
public record CreateKitchenResponse(
    Guid Id,
    string Name,
    string Code,
    double Rating,
    Guid Game
);