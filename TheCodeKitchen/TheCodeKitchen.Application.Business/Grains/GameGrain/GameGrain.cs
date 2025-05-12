
using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Contants;


namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain(
    [PersistentState(TheCodeKitchenStorage.Game)] IPersistentState<Game> state,
    IMapper mapper,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;