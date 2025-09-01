namespace TheCodeKitchen.Application.Constants;

public readonly record struct MinimumTimeBetweenOrders
{
    public static TimeSpan Minimum { get; } = TimeSpan.FromMinutes(1);
    public static TimeSpan Maximum { get; } = TimeSpan.FromHours(4);
}