namespace TheCodeKitchen.Application.Contracts.Events.Kitchen;

public record KitchenCreatedEventDto(
    Guid Id,
    string Name,
    string Code
) : DomainEventDto;