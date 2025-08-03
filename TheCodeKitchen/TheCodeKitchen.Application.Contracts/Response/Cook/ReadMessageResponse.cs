namespace TheCodeKitchen.Application.Contracts.Response.Cook;

public record ReadMessageResponse(int Number, string From, string To, string Content, DateTimeOffset Timestamp);