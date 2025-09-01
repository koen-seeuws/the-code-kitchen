namespace TheCodeKitchen.Application.Contracts.Response.Game;

[GenerateSerializer]
public record GetGameResponse(
    Guid Id,
    string Name,
    TimeSpan TimePerMoment,
    double SpeedModifier,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature,
    DateTimeOffset? Started,
    TimeSpan TimePassed,
    bool Paused
);