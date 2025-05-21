using Orleans.Placement;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;


[PreferLocalPlacement]
public partial class KitchenGrain(
    [PersistentState(TheCodeKitchenStorage.Kitchen, TheCodeKitchenStorage.Kitchen)] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain
{
    
}