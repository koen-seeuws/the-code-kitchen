using TheCodeKitchen.Server.Core.Enums;

namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }

    public ICollection<MenuItem> MenuItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}