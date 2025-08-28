namespace TheCodeKitchen.Application.Constants;

public readonly record struct TheCodeKitchenMinimumTimeBetweenOrders
{
    public static TimeSpan Value { get; } = TimeSpan.FromMinutes(10);
}