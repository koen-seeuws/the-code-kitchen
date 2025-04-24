using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public sealed class GamePausedOrUnpausedNotificationHandler(
    IMapper mapper,
    IRealtimeGameManagementService realtimeGameManagementService
) : INotificationHandler<GamePausedOrUnpausedNotification>
{
    public async Task Handle(GamePausedOrUnpausedNotification notification, CancellationToken cancellationToken)
    {
        var gamePausedOrUnpausedEventDto = mapper.Map<GamePausedOrUnpausedEventDto>(notification);
        await realtimeGameManagementService
            .GamePausedOrUnpausedNotification(notification.GameId, gamePausedOrUnpausedEventDto, cancellationToken);
    }
}