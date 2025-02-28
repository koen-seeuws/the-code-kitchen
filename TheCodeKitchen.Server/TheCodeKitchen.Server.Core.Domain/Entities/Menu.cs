namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
}