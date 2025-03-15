namespace TheCodeKitchen.Application.Contracts.Events;

public abstract record DomainEventDto
{
    public DateTimeOffset Occured { get; }
}