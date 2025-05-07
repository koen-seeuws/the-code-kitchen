namespace TheCodeKitchen.Application.Contracts.Requests;

[GenerateSerializer]
public record NextKitchenMomentRequest(DateTimeOffset Moment);