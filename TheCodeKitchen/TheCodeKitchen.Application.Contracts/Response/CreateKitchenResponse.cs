using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record CreateKitchenResponse(
    Guid Id,
    string Name,
    string Code
);