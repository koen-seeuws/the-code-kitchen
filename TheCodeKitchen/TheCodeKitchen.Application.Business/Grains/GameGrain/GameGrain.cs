using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain(
    [PersistentState(TheCodeKitchenState.Games, TheCodeKitchenState.Games)] IPersistentState<Game> state,
    IMapper mapper,
    IRealTimeGameService realTimeGameService
) : Grain, IGameGrain;