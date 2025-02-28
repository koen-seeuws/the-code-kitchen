namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class OrderItem
{
    public long Id { get; set; }
    

    public long OrderId { get; set; }
    public Order Order { get; set; }

    public string ItemId { get; set; }
    public Item Item { get; set; }
}