using Microsoft.Extensions.Logging;
using Orleans.Placement;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public class KitchenGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
    public StreamSubscriptionHandle<NewOrderEvent>? NewOrderStreamSubscriptionHandle { get; set; }
    public StreamSubscriptionHandle<KitchenOrderRatingUpdatedEvent>? KitchenOrderRatingUpdatedStreamSubscriptionHandle { get; set; }
}

[PreferLocalPlacement]
public sealed partial class KitchenGrain(
    [PersistentState(TheCodeKitchenState.Kitchens, TheCodeKitchenState.Kitchens)]
    IPersistentState<Kitchen> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<KitchenGrainStreamSubscriptionHandles> streamHandles,
    IMapper mapper,
    ILogger<KitchenGrain> logger
) : Grain, IKitchenGrain;