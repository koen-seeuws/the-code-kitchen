using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public class GameStartedNotificationHandler(
    IRealtimeGameManagementService realtimeGameManagementService
) : INotificationHandler<GameStartedNotification>
{
    public async Task Handle(GameStartedNotification notification, CancellationToken cancellationToken)
        => await realtimeGameManagementService.GameStarted(notification.GameId, cancellationToken);
}