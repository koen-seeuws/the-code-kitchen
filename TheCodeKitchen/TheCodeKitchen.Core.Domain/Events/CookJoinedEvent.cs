namespace TheCodeKitchen.Core.Domain.Events;

public record CookJoinedEvent(
    Guid Id,
    string Username,
    Guid GameId,
    Guid KitchenId
) : DomainEvent;