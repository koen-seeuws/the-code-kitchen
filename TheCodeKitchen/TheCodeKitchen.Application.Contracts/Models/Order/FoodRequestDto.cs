namespace TheCodeKitchen.Application.Contracts.Models;

[GenerateSerializer]
public record FoodRequestDto(string Food, TimeSpan MinimumTimeToPrepareFood);