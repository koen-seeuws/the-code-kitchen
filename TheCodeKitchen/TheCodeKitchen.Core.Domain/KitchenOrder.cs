namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder
{
    public long Number { get; set; }
    public Guid Kitchen { get; set; }
    
    public IDictionary<string, short> Deliveries { get; set; }
    
    public KitchenOrder(long number, Guid kitchen)
    {
        Number = number;
        Kitchen = kitchen;
        Deliveries = new Dictionary<string, short>();
    }
}