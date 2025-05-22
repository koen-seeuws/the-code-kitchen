namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class EquipmentGrain
{
    private StreamSubscriptionHandle<NextMomentEvent>? _nextMomentEventSubscription;

    private async Task SubscribeToNextMomentEvent()
    {
        if (state.RecordExists)
        {
            var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.AzureStorageQueuesProvider);
            var stream = streamProvider.GetStream<NextMomentEvent>(state.State.Game);
            _nextMomentEventSubscription = await stream.SubscribeAsync(async events =>
                {
                    foreach (var @event in events)
                    {
                        await OnNextMomentEvent(@event.Item); // await one by one, preserves order
                    }
                }
            );
        }
    }
}