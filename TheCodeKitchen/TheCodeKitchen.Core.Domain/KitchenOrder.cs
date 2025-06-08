namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder(long number, Guid kitchen)
{
    public long Number { get; } = number;
    public Guid Kitchen { get; } = kitchen;
    public ICollection<Guid> DeliveredFoods { get; } = new List<Guid>();
}