namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public partial class CookBookGrain(
    [PersistentState(TheCodeKitchenState.CookBook, TheCodeKitchenState.CookBook)]
    IPersistentState<CookBook> state,
    IMapper mapper
) : Grain, ICookBookGrain;