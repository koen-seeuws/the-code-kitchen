using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Application.Business.Grains.CookBookGrain;

public sealed partial class CookBookGrain(
    [PersistentState(TheCodeKitchenState.CookBook, TheCodeKitchenState.CookBook)]
    IPersistentState<CookBook> state,
    IMapper mapper
) : Grain, ICookBookGrain;