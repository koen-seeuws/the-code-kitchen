namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    public sealed override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        streamHandles.State ??= new KitchenGrainStreamSubscriptionHandles();
        await SubscribeToNextMomentEvent();
        await SubscribeToNewOrderEvent();
        await SubscribeToKitchenOrderRatingUpdatedEvent();
        await base.OnActivateAsync(cancellationToken);
    }
}