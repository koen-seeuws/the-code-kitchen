namespace TheCodeKitchen.Application.Contracts.Events.KitchenOrder;

[GenerateSerializer]
public record KitchenOrderFoodDeliveredEvent(long Number, string FoodName, double Rating);