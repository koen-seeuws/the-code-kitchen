namespace TheCodeKitchen.Core.Domain.Events;

public record GameStartedEvent(Guid GameId) : DomainEvent;