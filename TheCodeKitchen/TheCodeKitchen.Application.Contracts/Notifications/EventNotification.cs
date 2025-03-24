namespace TheCodeKitchen.Application.Contracts.Notifications;

public abstract record EventNotification
{
    public DateTimeOffset Occured { get; } = DateTimeOffset.UtcNow;
}