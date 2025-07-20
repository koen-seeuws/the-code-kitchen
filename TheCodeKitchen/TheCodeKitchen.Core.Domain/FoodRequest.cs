namespace TheCodeKitchen.Core.Domain;

public record FoodRequest(string food, TimeSpan minimumTimeToPrepareFood)
{
    public string RequestedFood { get; init; } = food;
    public TimeSpan MinimumTimeToPrepareFood { get; set; } = minimumTimeToPrepareFood;
}