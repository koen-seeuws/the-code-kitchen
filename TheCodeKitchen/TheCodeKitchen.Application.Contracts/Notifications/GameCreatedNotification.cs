using MediatR;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Interfaces.Events;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record GameCreatedNotification(
    Guid Id,
    string Name
) : DomainEventDto, INotification;