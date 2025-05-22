using TheCodeKitchen.Application.Contracts.Grains.Equipment;

namespace TheCodeKitchen.Application.Business.Grains.EquipmentGrain;

public abstract partial class EquipmentGrain(
    IPersistentState<Core.Domain.Equipment> state,
    IMapper mapper,
    short maxItems
) : Grain, IEquipmentGrain;