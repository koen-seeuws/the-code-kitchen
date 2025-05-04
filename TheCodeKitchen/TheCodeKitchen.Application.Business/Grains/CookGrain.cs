using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class CookGrain(
    [PersistentState("Cook")] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;