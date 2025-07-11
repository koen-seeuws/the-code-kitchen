namespace TheCodeKitchen.Application.Contracts.Contants;

public readonly record struct TheCodeKitchenMomentDuration
{
    public static TimeSpan Value { get; } = TimeSpan.FromSeconds(60);
}