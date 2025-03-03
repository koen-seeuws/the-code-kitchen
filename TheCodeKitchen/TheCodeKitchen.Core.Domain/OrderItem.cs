namespace TheCodeKitchen.Core.Domain;

public class OrderItem
{
    public short Quantity { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; }

    public string ItemId { get; set; }
    public Item Item { get; set; }
}