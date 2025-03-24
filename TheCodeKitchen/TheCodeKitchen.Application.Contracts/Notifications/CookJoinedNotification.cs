using MediatR;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record CookJoinedNotification(
    Guid Id,
    string Username,
    Guid GameId,
    Guid KitchenId
) : EventNotification, INotification;