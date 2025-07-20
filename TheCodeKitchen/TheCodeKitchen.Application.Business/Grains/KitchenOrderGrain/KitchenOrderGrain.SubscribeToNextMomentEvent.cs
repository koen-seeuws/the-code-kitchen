namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        try
        {
            streamHandles.State.NextMomentStreamSubscriptionHandle = await streamHandles.State.NextMomentStreamSubscriptionHandle.ResumeAsync(OnNextMomentEvent);
            await streamHandles.WriteStateAsync();
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), state.State.Game);
                streamHandles.State.NextMomentStreamSubscriptionHandle = await stream.SubscribeAsync(OnNextMomentEvent);
                await streamHandles.WriteStateAsync();
            }
        }
    } 
}