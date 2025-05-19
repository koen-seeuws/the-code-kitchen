namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    private int? _secondsUntilNewOrder = null;
    
    private Task PickSecondsUntilNextOrder()
    {
        _secondsUntilNewOrder = Random.Shared.Next(30, 300);
        return Task.CompletedTask;
    }
}