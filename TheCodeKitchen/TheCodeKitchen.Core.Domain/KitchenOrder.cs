namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder(long number, Guid game, Guid kitchen)
{
    public long Number { get; } = number;
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public bool Completed { get; set; }
    public Guid Game { get; set; } = game;
    public Guid Kitchen { get; } = kitchen;
    public ICollection<Guid> DeliveredFoods { get; } = new List<Guid>();

}