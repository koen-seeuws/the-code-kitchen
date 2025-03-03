namespace TheCodeKitchen.Core.Domain;

public class Game
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Paused { get; set; }

    //Navigation properties
    public ICollection<Kitchen> Kitchens { get; set; }
    public ICollection<Table> Tables { get; set; }
}