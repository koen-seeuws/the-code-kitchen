namespace TheCodeKitchen.Application.Contracts.Response.Kitchen;

[GenerateSerializer]
public record JoinKitchenResponse(
    Guid GameId,
    Guid KitchenId,
    Guid CookId,
    string Username,
    string PasswordHash,
    bool isNewCook
);