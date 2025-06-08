using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain
{
    public async Task<Result<CreateKitchenOrderResponse>> Initialize(CreateKitchenOrderRequest request)
    {
        var orderNumber = this.GetPrimaryKeyLong();
        var kitchen = Guid.Parse(this.GetPrimaryKeyString().Split('+')[1]);
        
        if(state.RecordExists)
            return new AlreadyExistsError($"The order with number {orderNumber} and kitchen {kitchen} has already been initialized");

        var kitchenOrder = new KitchenOrder(orderNumber, kitchen);
        state.State = kitchenOrder;
        await state.WriteStateAsync();
        
        return mapper.Map<CreateKitchenOrderResponse>(kitchenOrder);
    }
}