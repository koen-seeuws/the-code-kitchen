namespace TheCodeKitchen.Application.Contracts.Contants;

public readonly record struct TheCodeKitchenMomentDuration
{
    public static TimeSpan Value { get; set; } = TimeSpan.FromSeconds(60);
}