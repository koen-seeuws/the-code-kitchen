using TheCodeKitchen.Application.Contracts.Grains.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain(
    IPersistentState<Equipment> state,
    IMapper mapper,
    short maxItems
) : Grain, IEquipmentGrain;