namespace TheCodeKitchen.Server.Core.Domain;

public class Game
{
    public int Id { get; set; }

    //Navigation properties
    public ICollection<Kitchen> Kitchens { get; set; }
}