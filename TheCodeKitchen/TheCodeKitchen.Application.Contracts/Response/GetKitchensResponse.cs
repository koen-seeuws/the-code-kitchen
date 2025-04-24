namespace TheCodeKitchen.Application.Contracts.Response;

public record GetKitchensResponse(Guid Id, string Name, string? Code);