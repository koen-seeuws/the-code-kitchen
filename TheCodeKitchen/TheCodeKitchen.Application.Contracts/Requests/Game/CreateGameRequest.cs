namespace TheCodeKitchen.Application.Contracts.Requests.Game;

[GenerateSerializer]
public record CreateGameRequest(string? Name, double SpeedModifier, short MinimumItemsPerOrder, short MaximumItemsPerOrder,  double OrderSpeedModifier, double Temperature);