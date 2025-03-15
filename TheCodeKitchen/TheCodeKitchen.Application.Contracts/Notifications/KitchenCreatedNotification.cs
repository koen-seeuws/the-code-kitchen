using MediatR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Events;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record KitchenCreatedNotification(
    Guid Id,
    string Name,
    string Code,
    Guid GameId
) : DomainEventDto, INotification;