namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder
{
    public long Number { get; set; }
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public bool Completed { get; set; }
    public double CompletenessRating { get; set; } = 0.0;

    public Guid Game { get; set; }
    public Guid Kitchen { get; set; }

    public ICollection<KitchenOrderFoodRequest> RequestedFoods { get; set; }

    public ICollection<KitchenOrderFoodDelivery> DeliveredFoods { get; set; } = new List<KitchenOrderFoodDelivery>();

    public KitchenOrder()
    {
    }

    public KitchenOrder(ICollection<OrderFoodRequest>? requestedFoods, long number, Guid game, Guid kitchen)
    {
        Number = number;
        Game = game;
        Kitchen = kitchen;
        RequestedFoods = requestedFoods?
            .Select(fr => new KitchenOrderFoodRequest(fr.Food, fr.MinimumTimeToPrepareFood))
            .ToList() ?? [];
    }
}