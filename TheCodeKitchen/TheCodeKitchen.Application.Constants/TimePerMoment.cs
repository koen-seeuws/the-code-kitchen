namespace TheCodeKitchen.Application.Constants;

public readonly record struct TimePerMoment
{
    public static TimeSpan Minimum { get; set; } = TimeSpan.FromSeconds(1);
    public static TimeSpan Maximum { get; set; } = TimeSpan.FromMinutes(1);
}