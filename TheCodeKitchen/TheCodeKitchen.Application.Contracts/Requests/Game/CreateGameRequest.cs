namespace TheCodeKitchen.Application.Contracts.Requests.Game;

[GenerateSerializer]
public record CreateGameRequest(
    string? Name,
    TimeSpan TimePerMoment,
    double SpeedModifier,
    TimeSpan MinimumTimeBetweenOrders,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature
);