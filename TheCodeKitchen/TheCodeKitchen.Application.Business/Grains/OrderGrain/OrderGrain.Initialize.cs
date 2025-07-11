using TheCodeKitchen.Application.Contracts.Requests.Order;
using TheCodeKitchen.Application.Contracts.Response.Order;

namespace TheCodeKitchen.Application.Business.Grains.OrderGrain;

public sealed partial class OrderGrain
{
    public async Task<Result<CreateOrderResponse>> Initialize(CreateOrderRequest request)
    {
        var orderNumber = this.GetPrimaryKeyLong();
        var game = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The order with number {orderNumber} and game {game} has already been initialized");

        
        
        
        
        
        var order = new Order(orderNumber, game);
        state.State = order;
        await state.WriteStateAsync();
        
        return mapper.Map<CreateOrderResponse>(order);
    }
}