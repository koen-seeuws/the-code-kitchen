namespace TheCodeKitchen.Server.Core.Domain.Entities;

public class Game
{
    public int Id { get; set; }

    //Navigation properties
    public ICollection<Kitchen> Kitchens { get; set; }
}