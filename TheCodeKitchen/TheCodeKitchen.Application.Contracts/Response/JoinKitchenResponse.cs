namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record JoinKitchenResponse(
    Guid GameId,
    Guid KitchenId,
    Guid CookId,
    string Username,
    string PasswordHash,
    bool isNewCook
);