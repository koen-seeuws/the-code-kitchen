using TheCodeKitchen.Application.Contracts.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    private async Task SubscribeToNewOrderEvent()
    {
        if (streamHandles.State.NewOrderStreamSubscriptionHandle is not null)
        {
            streamHandles.State.NewOrderStreamSubscriptionHandle =
                await streamHandles.State.NewOrderStreamSubscriptionHandle.ResumeAsync(OnNewOrderEvent);
            await streamHandles.WriteStateAsync();
        }
        else if (state.RecordExists)
        {
            var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
            var stream = streamProvider.GetStream<NewOrderEvent>(nameof(NewOrderEvent), state.State.Game);
            streamHandles.State.NewOrderStreamSubscriptionHandle = await stream.SubscribeAsync(OnNewOrderEvent);
            await streamHandles.WriteStateAsync();
        }
    }
}