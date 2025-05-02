using MediatR;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record KitchenAddedNotification(
    Guid Id,
    string Name,
    string Code,
    Guid GameId
) : EventNotification, INotification;