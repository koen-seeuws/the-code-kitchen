namespace TheCodeKitchen.Core.Domain;

public class Order(long number, Guid game, ICollection<FoodRequest> requestedFoods)
{
    public long Number { get; set; } = number;
    public Guid Game { get; set; } = game;
    public ICollection<FoodRequest> RequestedFoods { get; set; } = requestedFoods;
}