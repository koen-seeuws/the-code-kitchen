using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Grains.Equipment;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public sealed partial class FurnaceGrain(
    [PersistentState(TheCodeKitchenState.Equipment, TheCodeKitchenState.Equipment)]
    IPersistentState<Equipment> state,
    IMapper mapper,
    ILogger<FurnaceGrain> logger
) : EquipmentGrain(state, mapper, 1), IFurnaceGrain
{
    private readonly IPersistentState<Equipment> _state = state;
}