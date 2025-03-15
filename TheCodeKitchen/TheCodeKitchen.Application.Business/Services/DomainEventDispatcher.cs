using TheCodeKitchen.Application.Contracts.Interfaces.Events;
using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Application.Business.Services;

public class DomainEventDispatcher(
    IMapper mapper,
    IMediator mediator
) : IDomainEventDispatcher
{
    public async Task DispatchEvents(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
        {
            var notification = mapper.Map<INotification>(@event);
            await mediator.Publish(notification);
        }
    }
}