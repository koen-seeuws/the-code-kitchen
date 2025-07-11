namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public partial class CookBookGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new CookBook(this.GetPrimaryKey());
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }
}