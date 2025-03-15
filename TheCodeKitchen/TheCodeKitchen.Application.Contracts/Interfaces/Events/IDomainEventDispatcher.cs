using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Events;

public interface IDomainEventDispatcher
{
    Task DispatchEvents(IEnumerable<DomainEvent> events);
}