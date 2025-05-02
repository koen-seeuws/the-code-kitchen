namespace TheCodeKitchen.Application.Contracts.Response;

public record GetGameResponse(Guid Id, string Name, DateTimeOffset? Started, DateTimeOffset? Paused);