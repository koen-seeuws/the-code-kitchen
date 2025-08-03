namespace TheCodeKitchen.Application.Contracts.Requests.Cook;

public record DeliverMessageToCookRequest(string From, string To, string Content, DateTimeOffset Timestamp);