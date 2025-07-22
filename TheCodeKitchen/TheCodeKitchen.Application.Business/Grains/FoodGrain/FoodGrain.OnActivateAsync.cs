namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        streamSubscriptionHandles.State ??= new FoodGrainStreamSubscriptionHandles();
        await SubscribeToNextMomentEvent();
        await base.OnActivateAsync(cancellationToken);
    }
}