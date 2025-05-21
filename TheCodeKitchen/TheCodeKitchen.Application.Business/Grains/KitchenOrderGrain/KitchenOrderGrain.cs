using TheCodeKitchen.Application.Contracts.Contants;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenStorage.KitchenOrder, TheCodeKitchenStorage.KitchenOrder)]
    IPersistentState<KitchenOrder> state,
    IMapper mapper
) : Grain, IKitchenOrderGrain;