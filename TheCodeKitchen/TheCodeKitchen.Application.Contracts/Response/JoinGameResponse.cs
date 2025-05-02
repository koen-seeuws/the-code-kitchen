using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record JoinGameResponse(
    string Token
);