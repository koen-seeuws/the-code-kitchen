namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        try
        {
            await streamSubscriptionSubscriptionHAndleses.State.NextMomentStreamSubscriptionHandle
                .ResumeAsync(OnNextMomentEvent);
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.AzureStorageQueuesProvider);
                var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), state.State.Game);
                streamSubscriptionSubscriptionHAndleses.State.NextMomentStreamSubscriptionHandle =
                    await stream.SubscribeAsync(OnNextMomentEvent);
                await streamSubscriptionSubscriptionHAndleses.WriteStateAsync();
            }
        }
    }
}