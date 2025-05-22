namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record NextMomentEvent(Guid GameId, DateTimeOffset Moment);