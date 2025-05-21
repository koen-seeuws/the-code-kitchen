
using Microsoft.Extensions.Logging;
using Orleans.Timers;
using TheCodeKitchen.Application.Contracts.Contants;


namespace TheCodeKitchen.Application.Business.Grains.GameGrain;


public partial class GameGrain(
    [PersistentState(TheCodeKitchenStorage.Game, TheCodeKitchenStorage.Game)] IPersistentState<Game> state,
    IMapper mapper,
    ITimerRegistry timerRegistry,
    IReminderRegistry reminderRegistry,
    ILogger<GameGrain> logger
) : Grain, IGameGrain;