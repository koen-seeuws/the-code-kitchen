using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

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