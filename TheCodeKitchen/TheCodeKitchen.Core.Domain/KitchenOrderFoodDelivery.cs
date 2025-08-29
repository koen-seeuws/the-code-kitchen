namespace TheCodeKitchen.Core.Domain;

public class KitchenOrderFoodDelivery(Food food, double rating)
{
    public Food Food { get; set; } = food;
    public double Rating { get; set; } = rating;
}