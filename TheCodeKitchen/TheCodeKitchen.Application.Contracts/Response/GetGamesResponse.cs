namespace TheCodeKitchen.Application.Contracts.Response;

public record GetGamesResponse(Guid Id, string Name, DateTimeOffset? Started, DateTimeOffset? Paused);