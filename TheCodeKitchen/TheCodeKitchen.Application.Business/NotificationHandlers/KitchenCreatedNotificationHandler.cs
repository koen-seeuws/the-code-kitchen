using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;
using TheCodeKitchen.Application.Contracts.Notifications;

namespace TheCodeKitchen.Application.Business.NotificationHandlers;

public class KitchenCreatedNotificationHandler(
    IMapper mapper,
    IRealtimeKitchenManagementService realtimeKitchenManagementService
) : INotificationHandler<KitchenCreatedNotification>
{
    public Task Handle(KitchenCreatedNotification notification, CancellationToken cancellationToken)
    {
        var kitchenCreatedEventDto = mapper.Map<KitchenCreatedEventDto>(notification);
        return realtimeKitchenManagementService.KitchenCreated(notification.GameId, kitchenCreatedEventDto);
    }
}