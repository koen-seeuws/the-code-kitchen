using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenGrain(
    [PersistentState("Kitchen")] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain;