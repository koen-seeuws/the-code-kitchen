using MediatR;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record GameCreatedNotification(
    Guid Id,
    string Name
) : EventNotification, INotification;