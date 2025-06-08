namespace TheCodeKitchen.Core.Domain;

public class Equipment(Guid game, Guid kitchen, int number)
{
    public Guid Game { get; } = game;
    public Guid Kitchen { get; } = kitchen;
    public int Number { get; } = number;
    public IList<Guid> Foods { get; set; }
}