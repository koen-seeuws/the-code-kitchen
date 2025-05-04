using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record JoinKitchenResponse(
    Guid CookId,
    string Username,
    Guid KitchenId,
    string PasswordHash,
    bool isNewCook
);