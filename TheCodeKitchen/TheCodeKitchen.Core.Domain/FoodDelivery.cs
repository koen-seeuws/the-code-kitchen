namespace TheCodeKitchen.Core.Domain;

public class FoodDelivery(Guid foodId, string food, double rating)
{
    public Guid FoodId { get; set; } = foodId;
    public string Food { get; set; } = food;
    public double Rating { get; set; } = rating;
}