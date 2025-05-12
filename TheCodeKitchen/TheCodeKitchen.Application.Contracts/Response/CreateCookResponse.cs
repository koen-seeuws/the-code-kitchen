namespace TheCodeKitchen.Application.Contracts.Response;

[GenerateSerializer]
public record CreateCookResponse(Guid Id, string Username);