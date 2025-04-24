namespace TheCodeKitchen.Application.Contracts.Events.Game;

public record GamePausedOrUnpausedEventDto(DateTimeOffset? Paused);