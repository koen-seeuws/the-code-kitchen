using Orleans;

namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record GetCookRequest(string? Username);