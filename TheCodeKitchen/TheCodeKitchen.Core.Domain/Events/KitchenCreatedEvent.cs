using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain.Events;

public record KitchenCreatedEvent(
    Guid Id,
    string Name,
    string Code,
    Guid GameId
) : DomainEvent;