namespace TheCodeKitchen.Application.Contracts.Requests;

public record CreateCookRequest(string Username, string PasswordHash, Guid KitchenId);