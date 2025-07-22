namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        streamHandles.State ??= new KitchenOrderGrainStreamSubscriptionHandles();
        await SubscribeToNextMomentEvent();
        await base.OnActivateAsync(cancellationToken);
    }
}