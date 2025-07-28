using TheCodeKitchen.Application.Contracts.Constants;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public class EquipmentGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public partial class EquipmentGrain(
    [PersistentState(TheCodeKitchenState.Equipment, TheCodeKitchenState.Equipment)]
    IPersistentState<Equipment> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<EquipmentGrainStreamSubscriptionHandles> streamSubscriptionHandles,
    IMapper mapper
) : Grain, IEquipmentGrain;