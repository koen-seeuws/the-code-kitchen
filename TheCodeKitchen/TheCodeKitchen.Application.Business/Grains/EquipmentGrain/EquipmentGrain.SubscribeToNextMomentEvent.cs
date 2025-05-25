namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain
{
    protected StreamSubscriptionHandle<NextMomentEvent>? NextMomentEventSubscription;

    private async Task SubscribeToNextMomentEvent()
    {
        if (state.RecordExists)
        {
            var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.AzureStorageQueuesProvider);
            var stream = streamProvider.GetStream<NextMomentEvent>(state.State.Game);
            NextMomentEventSubscription = await stream.SubscribeAsync(async events =>
                {
                    foreach (var @event in events)
                    {
                        await OnNextMomentEvent(@event.Item);
                    }
                }
            );
        }
    }
}