namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Trash()
    {
        if (streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle is not null)
            await streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle.UnsubscribeAsync();
        
        await streamSubscriptionHandles.ClearStateAsync();
        await state.ClearStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}