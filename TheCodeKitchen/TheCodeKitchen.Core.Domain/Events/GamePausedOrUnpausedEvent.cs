namespace TheCodeKitchen.Core.Domain.Events;

public record GamePausedOrUnpausedEvent(Guid GameId, DateTimeOffset? Paused) : DomainEvent;