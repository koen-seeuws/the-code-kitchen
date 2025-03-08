using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain.Events;

public class KitchenCreatedEvent(long? id, string name, string code) : IDomainEvent
{
    
}