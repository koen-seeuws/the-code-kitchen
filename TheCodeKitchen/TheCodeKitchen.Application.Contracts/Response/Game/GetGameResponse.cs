namespace TheCodeKitchen.Application.Contracts.Response.Game;

[GenerateSerializer]
public record GetGameResponse(
    Guid Id,
    string Name,
    double SpeedModifier,
    DateTimeOffset? Started,
    bool Paused
);