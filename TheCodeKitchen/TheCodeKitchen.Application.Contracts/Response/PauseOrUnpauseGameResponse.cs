namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record PauseOrUnpauseGameResponse(bool Paused);