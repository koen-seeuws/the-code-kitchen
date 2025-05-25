namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenState.KitchenOrder, TheCodeKitchenState.KitchenOrder)]
    IPersistentState<KitchenOrder> state,
    IMapper mapper
) : Grain, IKitchenOrderGrain;