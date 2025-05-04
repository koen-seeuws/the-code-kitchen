namespace TheCodeKitchen.Application.Business.Grains.KitchenManagementGrain;

public partial class KitchenManagementGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new KitchenManagementState();
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }
}