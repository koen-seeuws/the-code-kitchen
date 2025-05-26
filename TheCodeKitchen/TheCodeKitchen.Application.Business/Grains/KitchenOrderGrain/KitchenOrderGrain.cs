namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenState.KitchenOrders, TheCodeKitchenState.KitchenOrders)]
    IPersistentState<KitchenOrder> state,
    IMapper mapper
) : Grain, IKitchenOrderGrain;