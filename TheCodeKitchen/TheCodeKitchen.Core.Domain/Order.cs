namespace TheCodeKitchen.Core.Domain;

public class Order(long number, Guid game)
{
    public long Number { get; } = number;
    public Guid Game { get; } = game;
    public IDictionary<string, short> RequestedFoods { get; } = new Dictionary<string, short>();
}