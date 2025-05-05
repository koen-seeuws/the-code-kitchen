using TheCodeKitchen.Application.Business.Contants;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain(
    [PersistentState(Storage.Cook)] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain;