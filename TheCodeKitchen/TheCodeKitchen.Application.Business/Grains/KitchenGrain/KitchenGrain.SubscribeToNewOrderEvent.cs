namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task SubscribeToNewOrderEvent()
    {
        try
        {
            streamHandles.State.NewOrderStreamSubscriptionHandle = await streamHandles.State.NewOrderStreamSubscriptionHandle.ResumeAsync(OnNewOrderEvent);
            await streamHandles.WriteStateAsync();
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Game);
                streamHandles.State.NewOrderStreamSubscriptionHandle = await stream.SubscribeAsync(OnNewOrderEvent);
                await streamHandles.WriteStateAsync();
            }
        }
    }
}