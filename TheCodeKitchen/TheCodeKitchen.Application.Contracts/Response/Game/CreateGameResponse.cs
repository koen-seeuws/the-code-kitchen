namespace TheCodeKitchen.Application.Contracts.Response.Game;

[GenerateSerializer]
public record CreateGameResponse(
    Guid Id,
    string Name,
    TimeSpan TimePerMoment,
    double SpeedModifier,
    TimeSpan MinimumTimeBetweenOrders,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature
);