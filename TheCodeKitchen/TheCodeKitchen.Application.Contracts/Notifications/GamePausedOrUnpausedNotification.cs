using MediatR;

namespace TheCodeKitchen.Application.Contracts.Notifications;

public record GamePausedOrUnpausedNotification(Guid GameId, DateTimeOffset? Paused) : INotification;