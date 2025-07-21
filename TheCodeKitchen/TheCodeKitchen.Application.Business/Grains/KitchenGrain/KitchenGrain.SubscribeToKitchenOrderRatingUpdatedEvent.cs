namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task SubscribeToKitchenOrderRatingUpdatedEvent()
    {
        try
        {
            streamHandles.State.KitchenOrderRatingUpdatedStreamSubscriptionHandle = await streamHandles.State.KitchenOrderRatingUpdatedStreamSubscriptionHandle.ResumeAsync(OnKitchenOrderRatingUpdatedEvent);
            await streamHandles.WriteStateAsync();
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<KitchenOrderRatingUpdatedEvent>(nameof(KitchenOrderRatingUpdatedEvent), state.State.Id);
                streamHandles.State.KitchenOrderRatingUpdatedStreamSubscriptionHandle = await stream.SubscribeAsync(OnKitchenOrderRatingUpdatedEvent);
                await streamHandles.WriteStateAsync();
            }
        }
    }
}