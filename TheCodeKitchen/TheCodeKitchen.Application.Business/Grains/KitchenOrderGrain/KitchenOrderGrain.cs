namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenStorage.KitchenOrder, TheCodeKitchenStorage.KitchenOrder)]
    IPersistentState<KitchenOrder> state,
    IMapper mapper
) : Grain, IKitchenOrderGrain;