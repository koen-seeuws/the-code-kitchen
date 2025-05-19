namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record CreateGameRequest(string? Name, float SpeedModifier);