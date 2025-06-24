namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Trash()
    {
        if (streamHandles.State.NextMomentStreamSubscriptionHandle is not null)
            await streamHandles.State.NextMomentStreamSubscriptionHandle.UnsubscribeAsync();
        
        await streamHandles.ClearStateAsync();
        await state.ClearStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}