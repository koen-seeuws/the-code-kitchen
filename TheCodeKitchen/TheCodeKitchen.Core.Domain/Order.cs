namespace TheCodeKitchen.Core.Domain;

public class Order
{
    public long Number { get; set; }
    public Guid Game { get; set; }

    public Order(long number, Guid game)
    {
        Number = number;
        Game = game;
    }
}