using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

[GrainType(EquipmentTypes.Furnace)]
public sealed partial class FurnaceGrain(
    [PersistentState(TheCodeKitchenStorage.Equipment, TheCodeKitchenStorage.Equipment)]
    IPersistentState<Equipment> state,
    IMapper mapper
) : EquipmentGrain(state, mapper, 1);