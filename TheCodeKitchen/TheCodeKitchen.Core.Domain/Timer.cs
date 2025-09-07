namespace TheCodeKitchen.Core.Domain;

public sealed class Timer(int number, TimeSpan time, string? note)
{
    public int Number { get; set; } = number;
    public TimeSpan Time { get; set; } = time;
    public string? Note { get; set; } = note;
}