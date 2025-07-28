using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public class KitchenOrderGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public sealed partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenState.KitchenOrders, TheCodeKitchenState.KitchenOrders)]
    IPersistentState<KitchenOrder> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<KitchenOrderGrainStreamSubscriptionHandles> streamHandles,
    IMapper mapper
) : Grain, IKitchenOrderGrain;