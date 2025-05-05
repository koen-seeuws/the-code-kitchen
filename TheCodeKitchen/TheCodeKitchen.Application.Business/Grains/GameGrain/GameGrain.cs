
using Microsoft.Extensions.Logging;



namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain(
    [PersistentState(Storage.Game)] IPersistentState<Game> state,
    IMapper mapper,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;