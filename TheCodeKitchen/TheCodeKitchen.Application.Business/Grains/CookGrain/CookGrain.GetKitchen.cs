using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<GetKitchenResponse>> GetKitchen()
    {
        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(state.State.Kitchen);
        return await kitchenGrain.GetKitchen();
    }
}