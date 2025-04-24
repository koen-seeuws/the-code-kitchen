using MediatR;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record GameStartedNotification(Guid GameId) : INotification;