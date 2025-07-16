using TheCodeKitchen.Application.Business.Extensions;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        await SubscribeToNextMomentEvent();

        if (state.RecordExists)
        {
            // Load delivered food items into memory for quick access
            var getFoodTasks = state.State.DeliveredFoods
                .Select(foodId =>
                {
                    var foodGrain = GrainFactory.GetGrain<IFoodGrain>(foodId);
                    return foodGrain.GetFood();
                });

            var getFoodResults = await Task.WhenAll(getFoodTasks);
            var getFoodsResult = getFoodResults.Combine();
            _deliveredFoods = getFoodsResult.Value.ToList();

            // Load requested dishes from order
            var orderGrain = GrainFactory.GetGrain<IOrderGrain>(state.State.Number, state.State.Game.ToString());
            var getOrderResult = await orderGrain.GetOrder();
            _requestedFoodsWithTimeToPrepare = getOrderResult.Value.RequestedFoodsWithTimeToComplete;
        }

        await base.OnActivateAsync(cancellationToken);
    }
}