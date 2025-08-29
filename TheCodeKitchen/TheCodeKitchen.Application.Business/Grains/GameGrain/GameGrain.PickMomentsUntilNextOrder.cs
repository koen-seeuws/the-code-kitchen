namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    private TimeSpan? _timeUntilNewOrder;
    
    private Task PickRandomTimeUntilNextOrder()
    {
        var multiplier = Random.Shared.Next(1, 15);
        _timeUntilNewOrder = multiplier * TheCodeKitchenMomentDuration.Value;
        return Task.CompletedTask;
    }
}