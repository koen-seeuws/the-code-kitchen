using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public sealed class KitchenAddedNotificationHandler(
    IMapper mapper,
    IRealtimeGameManagementService realtimeGameManagementService
) : INotificationHandler<KitchenAddedNotification>
{
    public Task Handle(KitchenAddedNotification notification, CancellationToken cancellationToken)
    {
        var kitchenAddedEventDto = mapper.Map<KitchenAddedEventDto>(notification);
        return realtimeGameManagementService.KitchenCreated(notification.GameId, kitchenAddedEventDto);
    }
}