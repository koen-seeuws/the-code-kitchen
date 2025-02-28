namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Kitchen
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Navigation properties
    public int GameId { get; set; }
    public Game Game { get; set; }
    
    public ICollection<Cook> Cooks { get; set; }
}