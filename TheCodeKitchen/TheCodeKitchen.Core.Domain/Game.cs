namespace TheCodeKitchen.Core.Domain;

public class Game
{
    public Guid Id { get; }
    public string Name { get; set; }
    public float SpeedModifier { get; set; }
    public DateTimeOffset? Started { get; set; }
    public ICollection<Guid> Kitchens { get;  set; }
    public ICollection<long> OrderNumbers { get; set; }
    
    public Game(Guid id, string name, float speedModifier)
    {
        Id = id;
        Name = name;
        SpeedModifier = speedModifier;
        Kitchens = new List<Guid>();
        OrderNumbers = new List<long>();
    }
}