namespace TheCodeKitchen.Application.Contracts.Response;

public record CreateKitchenResponse(
    long Id,
    string Name,
    string Code
);