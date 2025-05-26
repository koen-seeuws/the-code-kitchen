using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Grains.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class FurnaceGrain(
    [PersistentState(TheCodeKitchenState.Equipment, TheCodeKitchenState.Equipment)]
    IPersistentState<Equipment> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<EquipmentGrainStreamSubscriptionHandles> streamSubscriptionHandles,
    IMapper mapper,
    ILogger<FurnaceGrain> logger
) : EquipmentGrain(state, streamSubscriptionHandles, mapper, 1), IFurnaceGrain
{
    private readonly IPersistentState<Equipment> _state = state;
}