using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.PantryGrain;

public partial class PantryGrain(
    [PersistentState(TheCodeKitchenState.Pantry, TheCodeKitchenState.Pantry)]
    IPersistentState<Pantry> state,
    IMapper mapper
) : Grain, IPantryGrain;