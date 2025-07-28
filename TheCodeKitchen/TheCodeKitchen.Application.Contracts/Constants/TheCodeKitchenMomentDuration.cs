namespace TheCodeKitchen.Application.Contracts.Constants;

public readonly record struct TheCodeKitchenMomentDuration
{
    public static TimeSpan Value { get; set; } = TimeSpan.FromSeconds(60);
}