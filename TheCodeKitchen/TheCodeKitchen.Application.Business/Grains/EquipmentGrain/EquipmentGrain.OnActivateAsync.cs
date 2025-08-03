namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class EquipmentGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        streamSubscriptionHandles.State ??= new EquipmentGrainStreamSubscriptionHandles();
        await SubscribeToNextMomentEvent();
        await base.OnActivateAsync(cancellationToken);
    }
}