namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record NextMomentEvent(Guid KitchenId, DateTimeOffset Moment);