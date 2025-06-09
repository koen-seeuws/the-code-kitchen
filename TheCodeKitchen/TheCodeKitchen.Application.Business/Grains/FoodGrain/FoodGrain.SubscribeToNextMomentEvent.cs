namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        try
        {
            await streamHandles.State.NextMomentStreamSubscriptionHandle.ResumeAsync(OnNextMomentEvent);
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent));
                streamHandles.State.NextMomentStreamSubscriptionHandle = await stream.SubscribeAsync(OnNextMomentEvent);
                await streamHandles.WriteStateAsync();
            }
        }
    } 
}