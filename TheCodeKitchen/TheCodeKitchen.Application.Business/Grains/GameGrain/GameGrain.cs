
using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Constants;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain(
    [PersistentState(TheCodeKitchenState.Games, TheCodeKitchenState.Games)] IPersistentState<Game> state,
    IMapper mapper,
    IRealTimeGameService realTimeGameService,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;