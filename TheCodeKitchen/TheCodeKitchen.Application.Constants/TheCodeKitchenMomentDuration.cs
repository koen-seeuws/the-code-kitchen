namespace TheCodeKitchen.Application.Constants;

public readonly record struct TheCodeKitchenMomentDuration
{
    public static TimeSpan Value { get; } = TimeSpan.FromSeconds(60);
}