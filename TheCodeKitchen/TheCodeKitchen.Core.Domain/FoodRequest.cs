namespace TheCodeKitchen.Core.Domain;

public class FoodRequest(string food, TimeSpan minimumTimeToPrepareFood)
{
    public string RequestedFood { get; set; } = food;
    public TimeSpan MinimumTimeToPrepareFood { get; set; } = minimumTimeToPrepareFood;
}