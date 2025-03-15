using MediatR;
using TheCodeKitchen.Application.Contracts.Events;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record GameCreatedNotification(
    Guid Id,
    string Name
) : DomainEventDto, INotification;