namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public class FoodGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public partial class FoodGrain(
    [PersistentState(TheCodeKitchenState.Food, TheCodeKitchenState.Food)]
    IPersistentState<Food> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<FoodGrainStreamSubscriptionHandles> streamHandles,
    IMapper mapper
) : Grain, IFoodGrain;