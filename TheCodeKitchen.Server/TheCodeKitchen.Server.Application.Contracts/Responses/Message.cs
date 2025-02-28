using TheCodeKitchen.Server.Core.Enums;

namespace TheCodeKitchen.Server.Application.Contracts.Responses;

public class Message
{
    public Message()
    {
        Timestamp = DateTimeOffset.UtcNow;
    }

    public Message(string? description, Reason reason = Reason.None, string? field = null)
    {
        Description = description;
        Reason = reason;
        Field = field;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public string? Field { get; init; }
    public string? Description { get; init; }
    public Reason Reason { get; init; }
    public DateTimeOffset? Timestamp { get; init; }
}