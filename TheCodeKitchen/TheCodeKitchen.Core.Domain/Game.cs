namespace TheCodeKitchen.Core.Domain;

public class Game(Guid id, string name, double speedModifier)
{
    public Guid Id { get; } = id;
    public string Name { get; set; } = name;
    public double SpeedModifier { get; set; } = speedModifier;
    public DateTimeOffset? Started { get; set; }
    public ICollection<Guid> Kitchens { get; } = new List<Guid>();
    public ICollection<long> OrderNumbers { get; } = new List<long>();
}