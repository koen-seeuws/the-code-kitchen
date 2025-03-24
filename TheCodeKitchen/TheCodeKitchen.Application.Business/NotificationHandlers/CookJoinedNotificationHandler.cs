using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public sealed class CookJoinedNotificationHandler(
    IMapper mapper,
    IRealtimeGameManagementService gameManagementService
) : INotificationHandler<CookJoinedNotification>
{
    public async Task Handle(CookJoinedNotification notification, CancellationToken cancellationToken)
    {
        var cookJoinedEventDto = mapper.Map<CookJoinedEventDto>(notification);
        await gameManagementService.CookJoined(notification.GameId, cookJoinedEventDto);
    }
}