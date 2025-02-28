namespace TheCodeKitchen.Server.Core.Domain;

public class Kitchen
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Cook> Cooks { get; set; }
}