namespace TheCodeKitchen.Application.Contracts.Events;

public record MessageReceivedEvent(int Number, string From, string To, string Content, DateTimeOffset Timestamp);