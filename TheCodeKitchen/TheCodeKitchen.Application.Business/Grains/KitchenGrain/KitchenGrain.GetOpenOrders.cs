using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Response.Order;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<IEnumerable<GetSimpleOrderResponse>>> GetOpenOrders()
    {
        if (!state.RecordExists)
            return new NotFoundError($"The kitchen with id {this.GetPrimaryKey()} does not exist");

        var tasks = state.State.OpenOrders.Select(async number =>
        {
            var orderGrain = GrainFactory.GetGrain<IOrderGrain>(number, state.State.Game.ToString());
            var result = await orderGrain.GetOrder();
            return result;
        });

        var results = await Task.WhenAll(tasks);
        var orders = results.Combine();
        
        if (!orders.Succeeded)
            return orders.Error;

        var simpleOrders = orders.Value
            .Select(o =>
            {
                var requestedFoods = o.RequestedFoods
                    .Select(f => f.RequestedFood)
                    .ToList();
                return new GetSimpleOrderResponse(o.Number, requestedFoods);
            })
            .ToList();

        return simpleOrders;
    }
}