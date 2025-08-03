namespace TheCodeKitchen.Application.Contracts.Response.Cook;

public record ReadMessageResponse(int Id, string From, string To, string Content, DateTime Timestamp);