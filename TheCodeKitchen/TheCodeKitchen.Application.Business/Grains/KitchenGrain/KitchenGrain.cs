namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain(
    [PersistentState(TheCodeKitchenStorage.Kitchen)] IPersistentState<Kitchen> state,
    IStreamProvider streamProvider,
    IMapper mapper
) : Grain, IKitchenGrain
{
    
}