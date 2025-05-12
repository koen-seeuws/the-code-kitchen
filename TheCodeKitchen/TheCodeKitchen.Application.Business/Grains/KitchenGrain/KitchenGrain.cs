using TheCodeKitchen.Application.Contracts.Contants;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain(
    [PersistentState(TheCodeKitchenStorage.Kitchen)] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain
{
    
}