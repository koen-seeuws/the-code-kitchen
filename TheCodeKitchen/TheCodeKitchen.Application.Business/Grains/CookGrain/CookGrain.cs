namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain(
    [PersistentState(TheCodeKitchenState.Cook, TheCodeKitchenState.Cook)] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;