namespace TheCodeKitchen.Core.Domain;

public class Order(long number, Guid game, ICollection<OrderFoodRequest> requestedFoods)
{
    public long Number { get; set; } = number;
    public Guid Game { get; set; } = game;
    public ICollection<OrderFoodRequest> RequestedFoods { get; set; } = requestedFoods;
}