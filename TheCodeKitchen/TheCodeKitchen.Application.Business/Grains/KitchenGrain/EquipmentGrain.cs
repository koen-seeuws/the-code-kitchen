using Orleans.Placement;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;


[PreferLocalPlacement]
public sealed partial class EquipmentGrain(
    [PersistentState(TheCodeKitchenStorage.Kitchen, TheCodeKitchenStorage.Kitchen)] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain
{
    
}