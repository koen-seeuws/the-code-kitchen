namespace TheCodeKitchen.Core.Domain.Events;

public record GameCreatedEvent(
    Guid Id,
    string Name
) : DomainEvent;