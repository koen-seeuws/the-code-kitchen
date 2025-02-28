namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public DateTimeOffset Ordered { get; set; }
    
    public long TableId { get; set; }
    public Table Table { get; set; }
    
    public ICollection<KitchenOrder> KitchenOrders { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}