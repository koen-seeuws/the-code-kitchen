namespace TheCodeKitchen.Core.Domain;

public class FoodRequest(string requestedFood, TimeSpan minimumTimeToPrepareFood)
{
    public string RequestedFood { get; set; } = requestedFood;
    public TimeSpan MinimumTimeToPrepareFood { get; set; } = minimumTimeToPrepareFood;
}