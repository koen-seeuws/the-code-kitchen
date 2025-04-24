using AutoMapper;
using MediatR;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Notifications;
using TheCodeKitchen.Core.Domain.Abstractions;
using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Infrastructure.Common.Services;

public class DomainEventDispatcher(
    IMapper mapper,
    IMediator mediator
) : IDomainEventDispatcher
{
    private static readonly Dictionary<Type, Type> NotificationMap = new()
    {
        [typeof(GameCreatedEvent)] = typeof(GameCreatedNotification),
        [typeof(KitchenAddedEvent)] = typeof(KitchenAddedNotification),
        [typeof(CookJoinedEvent)] = typeof(CookJoinedNotification),
        [typeof(GameStartedEvent)] = typeof(GameStartedNotification)
    };

    public async Task DispatchEvents(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var @event in events)
        {
            var eventType = @event.GetType();
            
            if (!NotificationMap.TryGetValue(eventType, out var notificationType))
                throw new NotSupportedException($"Event type {eventType.Name} is not supported.");
            
            var notification = mapper.Map(@event, eventType, notificationType);
            await mediator.Publish(notification, cancellationToken);
        }
    }

    public async Task DispatchAndClearEvents(DomainEntity domainEntity, CancellationToken cancellationToken = default)
    {
        await DispatchEvents(domainEntity.GetEvents(), cancellationToken);
        domainEntity.ClearEvents();
    }
}