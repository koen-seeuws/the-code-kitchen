namespace TheCodeKitchen.Application.Contracts.Requests.KitchenOrder;

[GenerateSerializer]
public record DeliverFoodRequest(Guid Cook);