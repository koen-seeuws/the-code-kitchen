using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain.Events;

public record GameCreatedEvent(long? Id, string Name) : IDomainEvent;