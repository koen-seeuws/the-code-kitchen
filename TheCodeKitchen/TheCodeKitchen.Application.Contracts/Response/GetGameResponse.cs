using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record GetGameResponse(
    Guid Id,
    string Name,
    double SpeedModifier,
    DateTimeOffset? Started,
    DateTimeOffset? Paused
);