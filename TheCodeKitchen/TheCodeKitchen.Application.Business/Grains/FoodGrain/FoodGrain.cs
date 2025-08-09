using TheCodeKitchen.Application.Contracts.Constants;
using TheCodeKitchen.Application.Contracts.Events.Game;

namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public class FoodGrainStreamSubscriptionHandles
{
    public StreamSubscriptionHandle<NextMomentEvent>? NextMomentStreamSubscriptionHandle { get; set; }
}

public sealed partial class FoodGrain(
    [PersistentState(TheCodeKitchenState.Food, TheCodeKitchenState.Food)]
    IPersistentState<Food> state,
    [PersistentState(TheCodeKitchenState.StreamHandles, TheCodeKitchenState.StreamHandles)]
    IPersistentState<FoodGrainStreamSubscriptionHandles> streamSubscriptionHandles,
    IMapper mapper
) : Grain, IFoodGrain;