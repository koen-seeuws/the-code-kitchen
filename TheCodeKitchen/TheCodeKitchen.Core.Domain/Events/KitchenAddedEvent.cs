namespace TheCodeKitchen.Core.Domain.Events;

public record KitchenAddedEvent(
    Guid Id,
    string Name,
    string Code,
    Guid GameId
) : DomainEvent;