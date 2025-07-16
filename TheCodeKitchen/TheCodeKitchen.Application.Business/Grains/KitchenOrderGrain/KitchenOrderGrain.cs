using TheCodeKitchen.Application.Contracts.Response.Food;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public class KitchenOrderGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public sealed partial class KitchenOrderGrain(
    [PersistentState(TheCodeKitchenState.KitchenOrders, TheCodeKitchenState.KitchenOrders)]
    IPersistentState<KitchenOrder> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<KitchenOrderGrainStreamSubscriptionHandles> streamHandles,
    IMapper mapper
) : Grain, IKitchenOrderGrain
{
    private ICollection<GetFoodResponse> _deliveredFoods = new List<GetFoodResponse>();
    private IDictionary<string, TimeSpan> _requestedFoodsWithTimeToPrepare = new Dictionary<string, TimeSpan>();
}