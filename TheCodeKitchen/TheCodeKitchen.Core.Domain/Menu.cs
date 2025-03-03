namespace TheCodeKitchen.Core.Domain;

public class Menu
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
}