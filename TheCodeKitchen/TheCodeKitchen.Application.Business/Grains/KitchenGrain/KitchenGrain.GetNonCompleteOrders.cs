using TheCodeKitchen.Application.Business.Extensions;
using TheCodeKitchen.Application.Contracts.Response.Order;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<IEnumerable<GetOrderResponse>>> GetOpenOrders()
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

        return results.Combine();
    }
}