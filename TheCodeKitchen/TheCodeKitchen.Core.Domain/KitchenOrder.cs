namespace TheCodeKitchen.Core.Domain;

public class KitchenOrder(ICollection<FoodRequest>? requestedFoods, long number, Guid game, Guid kitchen)
{
    public long Number { get; set; } = number;
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public bool Completed { get; set; }
    public double CompletenessRating { get; set; } = 0.0;

    public Guid Game { get; set; } = game;
    public Guid Kitchen { get; set; } = kitchen;

    public ICollection<FoodRequestRating> FoodRequestRatings { get; set; } =
        requestedFoods?
            .Select(fr => new FoodRequestRating(fr.RequestedFood, fr.MinimumTimeToPrepareFood))
            .ToList() ?? [];

    public ICollection<FoodDelivery> DeliveredFoods { get; set; } = new List<FoodDelivery>();
}