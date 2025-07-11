namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public partial class EquipmentGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        try
        {
            await streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle.ResumeAsync(OnNextMomentEvent);
        }
        catch (Exception e) when (e is OrleansException or NullReferenceException)
        {
            if (state.RecordExists)
            {
                var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
                var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent));
                streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle = await stream.SubscribeAsync(OnNextMomentEvent);
                await streamSubscriptionHandles.WriteStateAsync();
            }
        }
    } 
}