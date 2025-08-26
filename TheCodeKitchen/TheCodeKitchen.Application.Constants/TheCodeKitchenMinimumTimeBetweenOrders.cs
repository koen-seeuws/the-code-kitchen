namespace TheCodeKitchen.Application.Constants;

public readonly record struct TheCodeKitchenMinimumTimeBetweenOrders
{
    public static TimeSpan Value { get; set; } = TimeSpan.FromMinutes(10);
}