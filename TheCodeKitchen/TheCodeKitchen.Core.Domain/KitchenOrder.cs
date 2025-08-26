namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder
{
    public long Number { get; set; }
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public bool Completed { get; set; }
    public double CompletenessRating { get; set; } = 0.0;

    public Guid Game { get; set; }
    public Guid Kitchen { get; set; }

    public List<KitchenOrderFoodRequest> RequestedFoods { get; set; }

    public List<KitchenOrderFoodDelivery> DeliveredFoods { get; set; } = new();

    public KitchenOrder(List<KitchenOrderFoodRequest> requestedFoods, long number, Guid game, Guid kitchen)
    {
        Number = number;
        Game = game;
        Kitchen = kitchen;
        RequestedFoods = requestedFoods;
    }
}