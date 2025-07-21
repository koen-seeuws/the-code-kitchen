namespace TheCodeKitchen.Application.Contracts.Events;

[GenerateSerializer]
public record KitchenOrderRatingUpdatedEvent(double Rating);