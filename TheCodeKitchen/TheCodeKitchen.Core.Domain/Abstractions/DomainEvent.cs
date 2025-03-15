namespace TheCodeKitchen.Core.Domain.Abstractions;

public abstract record DomainEvent
{
    public DateTimeOffset Occurred { get; init; } = DateTimeOffset.UtcNow;
}