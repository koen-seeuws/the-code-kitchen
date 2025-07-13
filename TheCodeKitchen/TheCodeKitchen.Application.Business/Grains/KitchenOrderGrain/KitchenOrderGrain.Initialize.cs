using TheCodeKitchen.Application.Contracts.Requests.KitchenOrder;
using TheCodeKitchen.Application.Contracts.Response.KitchenOrder;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain
{
    public async Task<Result<CreateKitchenOrderResponse>> Initialize(CreateKitchenOrderRequest request)
    {
        var orderNumber = this.GetPrimaryKeyLong();
        var kitchen = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The order with number {orderNumber} has already been initialized in kitchen {kitchen}");

        var kitchenOrder = new KitchenOrder(orderNumber, request.Game, kitchen);
        state.State = kitchenOrder;
        await state.WriteStateAsync();
        
        return mapper.Map<CreateKitchenOrderResponse>(kitchenOrder);
    }
}