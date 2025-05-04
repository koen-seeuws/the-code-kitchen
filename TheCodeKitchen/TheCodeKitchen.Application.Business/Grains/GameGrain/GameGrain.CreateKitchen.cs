using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    public async Task<Result<CreateKitchenResponse>> CreateKitchen(CreateKitchenRequest request)
    {
        var id = Guid.CreateVersion7();
        state.State.Kitchens.Add(id);
        await state.WriteStateAsync();
        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(id);
        var result = await kitchenGrain.Initialize(request, state.State.Kitchens.Count);
        return result;
    }
}