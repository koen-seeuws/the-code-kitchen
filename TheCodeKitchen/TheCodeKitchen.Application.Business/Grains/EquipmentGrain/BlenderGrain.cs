using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Grains.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class BlenderGrain(
    [PersistentState(TheCodeKitchenState.Equipment, TheCodeKitchenState.Equipment)]
    IPersistentState<Equipment> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<EquipmentGrainStreamSubscriptionHandles> streamSubscriptionHandles,
    IMapper mapper,
    ILogger<BlenderGrain> logger
) : EquipmentGrain(state, streamSubscriptionHandles, mapper, 1), IBlenderGrain
{
    private readonly IPersistentState<Equipment> _state = state;
}