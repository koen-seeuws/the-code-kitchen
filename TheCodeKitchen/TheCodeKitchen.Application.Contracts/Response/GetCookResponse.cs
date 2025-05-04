using Orleans;

namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record GetCookResponse(Guid Id, string Username, string PasswordHash);