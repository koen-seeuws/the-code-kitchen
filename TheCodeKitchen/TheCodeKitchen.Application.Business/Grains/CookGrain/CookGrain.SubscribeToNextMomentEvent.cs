using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    private async Task SubscribeToNextMomentEvent()
    {
        if (streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle is not null)
        {
            logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Resuming subscription to NextMomentEvent...",
                this.GetPrimaryKey(), this.GetPrimaryKeyString());
            
            streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle =
                await streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle.ResumeAsync(OnNextMomentEvent);
            await streamSubscriptionHandles.WriteStateAsync();
            
            logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Resumed subscription to NextMomentEvent",
                this.GetPrimaryKey(), this.GetPrimaryKeyString());
        }
        else if (state.RecordExists)
        {
            logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Subscribing to NextMomentEvent...",
                this.GetPrimaryKey(), this.GetPrimaryKeyString());
            
            var streamProvider = this.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
            var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), state.State.Game);
            streamSubscriptionHandles.State.NextMomentStreamSubscriptionHandle =
                await stream.SubscribeAsync(OnNextMomentEvent);
            await streamSubscriptionHandles.WriteStateAsync();
            
            logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Subscribed to NextMomentEvent",
                this.GetPrimaryKey(), this.GetPrimaryKeyString());
        }
    }
}