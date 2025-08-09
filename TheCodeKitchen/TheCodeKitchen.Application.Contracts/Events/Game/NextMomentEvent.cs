namespace TheCodeKitchen.Application.Contracts.Events.Game;

[GenerateSerializer]
public record NextMomentEvent(Guid GameId, DateTimeOffset Moment, double Temperature);