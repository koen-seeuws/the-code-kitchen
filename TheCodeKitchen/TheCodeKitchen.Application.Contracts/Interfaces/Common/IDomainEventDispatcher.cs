using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Application.Contracts.Interfaces.Common;

public interface IDomainEventDispatcher
{
    Task DispatchEvents(IEnumerable<DomainEvent> events);
}