using Microsoft.Extensions.Logging;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain(
    [PersistentState("Game")] IPersistentState<Game> state,
    IMapper mapper,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;