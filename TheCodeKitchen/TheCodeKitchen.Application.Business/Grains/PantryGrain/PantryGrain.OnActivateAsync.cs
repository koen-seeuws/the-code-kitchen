namespace TheCodeKitchen.Application.Business.Grains.PantryGrain;

public partial class PantryGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new Pantry(this.GetPrimaryKey(), 12);
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }
}