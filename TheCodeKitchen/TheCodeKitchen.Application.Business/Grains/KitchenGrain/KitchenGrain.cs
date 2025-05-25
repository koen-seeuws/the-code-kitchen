using Microsoft.Extensions.Logging;
using Orleans.Placement;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

[ImplicitStreamSubscription("")]
[PreferLocalPlacement]
public sealed partial class KitchenGrain(
    [PersistentState(TheCodeKitchenState.Kitchen, TheCodeKitchenState.Kitchen)]
    IPersistentState<Kitchen> state,
    IMapper mapper,
    ILogger<KitchenGrain> logger
) : Grain, IKitchenGrain;