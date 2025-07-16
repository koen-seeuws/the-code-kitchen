namespace TheCodeKitchen.Core.Domain;

public class Order(long number, Guid game, IDictionary<string, TimeSpan> requestedFoodsWithTimeToComplete)
{
    public long Number { get; } = number;
    public Guid Game { get; } = game;
    public IDictionary<string, TimeSpan> RequestedFoodsWithTimeToCompleteWithTimeToComplete { get; } = requestedFoodsWithTimeToComplete;
}