using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record GetGameResponse(
    Guid Id,
    string Name,
    DateTimeOffset? Started,
    DateTimeOffset? Paused
);