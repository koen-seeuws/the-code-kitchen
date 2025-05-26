namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain(
    [PersistentState(TheCodeKitchenState.Cooks, TheCodeKitchenState.Cooks)] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;