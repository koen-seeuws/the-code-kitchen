using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;
using TheCodeKitchen.Application.Contracts.Interfaces.Realtime;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public class EquipmentGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public sealed partial class CookGrain(
    [PersistentState(TheCodeKitchenState.Cooks, TheCodeKitchenState.Cooks)] IPersistentState<Cook> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<EquipmentGrainStreamSubscriptionHandles> streamSubscriptionHandles,
    IMapper mapper,
    IRealTimeCookService realTimeCookService
) : Grain, ICookGrain;