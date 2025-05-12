namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record CreateGameResponse(Guid Id, string Name, double SpeedModifier);