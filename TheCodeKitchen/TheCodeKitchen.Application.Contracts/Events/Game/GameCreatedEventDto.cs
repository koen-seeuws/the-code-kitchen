namespace TheCodeKitchen.Application.Contracts.Events.Game;

public record GameCreatedEventDto(
    Guid Id,
    string Name
) : DomainEventDto;