namespace TheCodeKitchen.Application.Contracts.Requests.Cook;

[GenerateSerializer]
public record DeliverMessageToCookRequest(string From, string To, string Content, DateTimeOffset Timestamp);