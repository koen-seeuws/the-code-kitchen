namespace TheCodeKitchen.Application.Contracts.Response.Cook;

[GenerateSerializer]
public record CreateCookResponse(Guid Id, string Username);