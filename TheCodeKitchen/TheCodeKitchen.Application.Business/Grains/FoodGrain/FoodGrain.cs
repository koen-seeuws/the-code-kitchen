namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain(
    [PersistentState(TheCodeKitchenState.Food, TheCodeKitchenState.Food)]
    IPersistentState<Food> state,
    IMapper mapper
) : Grain, IFoodGrain;