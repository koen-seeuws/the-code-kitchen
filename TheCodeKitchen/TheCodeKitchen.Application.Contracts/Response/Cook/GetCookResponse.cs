namespace TheCodeKitchen.Application.Contracts.Response.Cook;

[GenerateSerializer]
public record GetCookResponse(Guid Id, string Username, string PasswordHash);