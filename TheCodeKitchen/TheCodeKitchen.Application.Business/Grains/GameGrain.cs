using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameGrain(
    [PersistentState("Game")] IPersistentState<Game> state,
    IMapper mapper,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;