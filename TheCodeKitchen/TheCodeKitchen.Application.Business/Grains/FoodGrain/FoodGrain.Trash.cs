namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public sealed partial class FoodGrain
{
    public async Task<Result<TheCodeKitchenUnit>> Trash()
    {
        if (streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle is not null)
        {
            try
            {
                await streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle.UnsubscribeAsync();
            }
            catch
            {
                // Ignore
            }
        }

        await streamSubscriptionHandles.ClearStateAsync();
        await state.ClearStateAsync();

        DeactivateOnIdle();

        return TheCodeKitchenUnit.Value;
    }
}