using AutoMapper;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Infrastructure.Common;

public class EventToNotificationMapping : Profile
{
    public EventToNotificationMapping()
    {
        CreateMap<GameCreatedEvent, GameCreatedNotification>();
        CreateMap<KitchenAddedEvent, KitchenAddedNotification>();
        CreateMap<CookJoinedEvent, CookJoinedNotification>();
        CreateMap<GameStartedEvent, GameStartedNotification>();
    }
}