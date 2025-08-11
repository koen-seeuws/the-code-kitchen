namespace TheCodeKitchen.Core.Domain;

public class KitchenOrderFoodDelivery(Guid foodId, string food, double rating)
{
    public Guid FoodId { get; set; } = foodId;
    public string Food { get; set; } = food;
    public double Rating { get; set; } = rating;
}