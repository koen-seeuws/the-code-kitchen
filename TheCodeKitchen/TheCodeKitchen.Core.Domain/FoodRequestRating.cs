namespace TheCodeKitchen.Core.Domain;

public class FoodRequestRating(string food, TimeSpan minimumTimeToPrepareFood)
{
    public string RequestedFood { get; init; } = food;
    public TimeSpan MinimumTimeToPrepareFood { get; set; } = minimumTimeToPrepareFood;
    public double Rating { get; set; } = 100.0;
    public bool Delivered { get; set; }
}