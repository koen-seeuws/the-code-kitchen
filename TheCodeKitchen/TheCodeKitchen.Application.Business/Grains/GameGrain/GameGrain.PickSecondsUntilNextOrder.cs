namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    private int? _momentsUntilNewOrder = null;
    
    private Task PickMomentsUntilNextOrder()
    {
        _momentsUntilNewOrder = Random.Shared.Next(30, 300);
        return Task.CompletedTask;
    }
}