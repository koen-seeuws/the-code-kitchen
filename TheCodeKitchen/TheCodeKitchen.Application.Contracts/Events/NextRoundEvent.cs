namespace TheCodeKitchen.Application.Contracts.Events;

public record NextMomentEvent(Guid KitchenId, DateTimeOffset Moment);