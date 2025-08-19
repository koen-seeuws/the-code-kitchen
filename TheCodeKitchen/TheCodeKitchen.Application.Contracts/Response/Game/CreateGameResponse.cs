namespace TheCodeKitchen.Application.Contracts.Response.Game;

[GenerateSerializer]
public record CreateGameResponse(
    Guid Id,
    string Name,
    double SpeedModifier,
    short MinimumItemsPerOrder,
    short MaximumItemsPerOrder,
    double OrderSpeedModifier,
    double Temperature
);