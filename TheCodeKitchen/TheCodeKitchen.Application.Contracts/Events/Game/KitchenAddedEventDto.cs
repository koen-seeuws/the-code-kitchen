namespace TheCodeKitchen.Application.Contracts.Events.Game;

public record KitchenAddedEventDto(
    Guid Id,
    string Name,
    string Code
) : DomainEventDto;