using Microsoft.Azure.Amqp.Framing;

namespace TheCodeKitchen.Application.Contracts.Requests.Food;

[GenerateSerializer]
public record CreateFoodRequest(string Name, double Temperature);