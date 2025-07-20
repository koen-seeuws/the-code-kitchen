namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder
{
    private KitchenOrder()
    {
    }

    public KitchenOrder(ICollection<FoodRequest> requestedFoods, long number, Guid game, Guid kitchen)
    {
        Number = number;
        Game = game;
        Kitchen = kitchen;
        FoodRequestRatings = requestedFoods
            .Select(fr => new FoodRequestRating(fr.RequestedFood, fr.MinimumTimeToPrepareFood))
            .ToList();
    }

    public long Number { get; }
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public bool Completed { get; set; }
    public double CompletenessRating { get; set; } = 0.0;

    public Guid Game { get; set; }
    public Guid Kitchen { get; }

    public ICollection<FoodRequestRating> FoodRequestRatings { get; set; }

    public ICollection<FoodDelivery> DeliveredFoods { get; set; } = new List<FoodDelivery>();
}