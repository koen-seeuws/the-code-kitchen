namespace TheCodeKitchen.Application.Contracts.Requests.Game;

[GenerateSerializer]
public record UpdateGameRequest(
    double SpeedModifier,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature
);