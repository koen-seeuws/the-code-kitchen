namespace TheCodeKitchen.Application.Contracts.Response;


[GenerateSerializer]
public record GetKitchenResponse(Guid Id, string Name, string? Code);