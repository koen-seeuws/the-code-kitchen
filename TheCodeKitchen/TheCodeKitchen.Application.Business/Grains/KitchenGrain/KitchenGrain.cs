namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain(
    [PersistentState("Kitchen")] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain;