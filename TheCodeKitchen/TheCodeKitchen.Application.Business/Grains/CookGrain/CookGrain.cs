namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain(
    [PersistentState("Cook")] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;