using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenManagementGrain
{
    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        var retrieved = state.State.KitchenCodes.TryGetValue(request.KitchenCode, out var kitchenId);

        if (!retrieved)
            return new NotFoundError($"The kitchen code {request.KitchenCode} does not exist");

        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(kitchenId);
        var result = await kitchenGrain.JoinKitchen(request);

        return result;
    }
}