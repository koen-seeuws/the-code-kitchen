using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ReleaseJoinCode()
    {
        if (string.IsNullOrWhiteSpace(state.State.Code))
            return new EmptyError();

        var kitchenCodeIndexGrain = GrainFactory.GetGrain<IKitchenCodeIndexGrain>(Guid.Empty);
        await kitchenCodeIndexGrain.DeleteKitchenCode(state.State.Code);

        state.State.Code = null;
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}