namespace TheCodeKitchen.Core.Domain.Abstractions;

public abstract class DomainEntity
{
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Modified { get; set; }
    private readonly ICollection<DomainEvent> _events  = new List<DomainEvent>();

    protected void RaiseEvent(DomainEvent @event) => _events.Add(@event);
    
    public ICollection<DomainEvent> GetEvents() => _events;
    
    public void ClearEvents() => _events.Clear();
}