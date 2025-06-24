namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public  partial class EquipmentGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        //await SubscribeToNextMomentEvent();
        await base.OnActivateAsync(cancellationToken);
    }
}