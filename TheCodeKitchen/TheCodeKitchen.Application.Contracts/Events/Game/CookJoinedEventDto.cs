namespace TheCodeKitchen.Application.Contracts.Events.Game;

public record CookJoinedEventDto(
    Guid Id,
    string Username,
    Guid KitchenId
);