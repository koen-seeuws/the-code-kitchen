namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record PauseOrResumeGameResponse(bool Paused);