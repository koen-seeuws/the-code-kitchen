namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain(
    [PersistentState(TheCodeKitchenStorage.Cook, TheCodeKitchenStorage.Cook)] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;