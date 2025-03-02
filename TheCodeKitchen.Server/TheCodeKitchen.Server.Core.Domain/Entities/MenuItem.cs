namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class MenuItem
{
    public double Price { get; set; }
    
    public long MenuId { get; set; }
    public Menu Menu { get; set; }

    public string ItemId { get; set; }
    public Item Item { get; set; }
}