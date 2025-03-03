namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder
{
    public long KitchenId { get; set; }
    public Kitchen Kitchen { get; set; }
    
    public long OrderId { get; set; }
    public Order Order { get; set; }
}