namespace TheCodeKitchen.Core.Domain.Abstractions;

public abstract class DomainEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
    public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();

    protected void RaiseEvent(IDomainEvent @event) => Events.Add(@event);
    
    protected void ClearEvents() => Events.Clear();
}