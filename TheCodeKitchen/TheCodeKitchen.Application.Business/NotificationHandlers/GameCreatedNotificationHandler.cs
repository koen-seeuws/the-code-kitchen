using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public class GameCreatedNotificationHandler(
    IMapper mapper,
    IRealtimeGameManagementService realtimeGameManagementService
) : INotificationHandler<GameCreatedNotification>
{
    public async Task Handle(GameCreatedNotification notification, CancellationToken cancellationToken)
    {
        var gameCreatedEventDto = mapper.Map<GameCreatedEventDto>(notification);
        await realtimeGameManagementService.GameCreated(gameCreatedEventDto);
    }
}