using Stream = System.IO.Stream;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain(
    [PersistentState(Storage.Kitchen)] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain;