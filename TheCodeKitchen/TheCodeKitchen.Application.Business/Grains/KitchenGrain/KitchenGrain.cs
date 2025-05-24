using Microsoft.Extensions.Logging;
using Orleans.Placement;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

[PreferLocalPlacement]
public sealed partial class KitchenGrain(
    [PersistentState(TheCodeKitchenStorage.Kitchen, TheCodeKitchenStorage.Kitchen)]
    IPersistentState<Kitchen> state,
    IMapper mapper,
    ILogger<KitchenGrain> logger
) : Grain, IKitchenGrain;