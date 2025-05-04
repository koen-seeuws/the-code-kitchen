using Orleans;

namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record JoinKitchenRequest(
    string Username,
    string PasswordHash,
    string KitchenCode
);