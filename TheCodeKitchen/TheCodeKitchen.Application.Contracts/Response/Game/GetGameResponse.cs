namespace TheCodeKitchen.Application.Contracts.Response.Game;

[GenerateSerializer]
public record GetGameResponse(
    Guid Id,
    string Name,
    double SpeedModifier,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature,
    DateTimeOffset? Started,
    TimeSpan Time,
    bool Paused
);