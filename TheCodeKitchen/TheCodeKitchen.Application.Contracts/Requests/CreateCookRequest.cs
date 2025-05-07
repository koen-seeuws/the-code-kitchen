namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record CreateCookRequest(string Username, string PasswordHash, Guid KitchenId);