using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain.Events;

public record GameCreatedEvent(Guid? Id, string Name) : IDomainEvent;