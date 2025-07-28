using Microsoft.Extensions.Logging;
using Orleans.Placement;
using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public class KitchenGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; } = null;
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