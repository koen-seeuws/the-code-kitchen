using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Grains.Equipment;
using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

[GrainType(EquipmentTypes.Blender)]
public sealed partial class BlenderGrain(
    [PersistentState(TheCodeKitchenStorage.Equipment, TheCodeKitchenStorage.Equipment)]
    IPersistentState<Equipment> state,
    IMapper mapper,
    ILogger<BlenderGrain> logger
) : EquipmentGrain(state, mapper, 1), IBlenderGrain
{
    private readonly IPersistentState<Equipment> _state = state;
}