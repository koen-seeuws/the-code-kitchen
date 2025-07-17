namespace TheCodeKitchen.Core.Domain;

public class Order(long number, Guid game, ICollection<FoodRequest> requestedFoods)
{
    public long Number { get; } = number;
    public Guid Game { get; } = game;
    public ICollection<FoodRequest> RequestedFoods { get; } = requestedFoods;
}