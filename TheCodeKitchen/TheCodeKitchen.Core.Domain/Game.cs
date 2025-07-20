namespace TheCodeKitchen.Core.Domain;

public class Game(Guid id, string name, double speedModifier = 1.0, double temperature = 30.0)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public double SpeedModifier { get; set; } = speedModifier;
    public double Temperature { get; set; } = temperature;
    public DateTimeOffset? Started { get; set; }
    public ICollection<Guid> Kitchens { get; set; } = new List<Guid>();
    public ICollection<long> OrderNumbers { get; set; } = new List<long>();
}