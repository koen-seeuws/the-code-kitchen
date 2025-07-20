
using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain(
    [PersistentState(TheCodeKitchenState.Games, TheCodeKitchenState.Games)] IPersistentState<Game> state,
    IMapper mapper,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;