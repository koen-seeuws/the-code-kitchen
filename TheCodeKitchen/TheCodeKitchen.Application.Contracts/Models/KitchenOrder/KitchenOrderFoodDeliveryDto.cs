namespace TheCodeKitchen.Application.Contracts.Models.KitchenOrder;

[GenerateSerializer]
public record KitchenOrderFoodDeliveryDto(Guid FoodId, string Food, double Rating);