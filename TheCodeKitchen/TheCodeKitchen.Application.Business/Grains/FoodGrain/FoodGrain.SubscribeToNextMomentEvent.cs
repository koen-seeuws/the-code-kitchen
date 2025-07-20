namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        try
        {
            streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle = await streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle.ResumeAsync(OnNextMomentEvent);
            await streamSubscriptionHandles.WriteStateAsync();
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), state.State.Game);
                streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle = await stream.SubscribeAsync(OnNextMomentEvent);
                await streamSubscriptionHandles.WriteStateAsync();
            }
        }
    } 
}