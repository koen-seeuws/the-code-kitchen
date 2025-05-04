using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenGrain
{
    public async Task<Result<CreateCookResponse>> CreateCook(CreateCookRequest request)
    {
        var cookResult = await GetCooks(new GetCookRequest(request.Username));
        if (!cookResult.Succeeded)
            return cookResult.Error;
        
        var cook = cookResult.Value.FirstOrDefault();
        if(cook is not null)
            return new AlreadyExistsError($"The cook with username {request.Username} already exists");

        var id = Guid.CreateVersion7();
        state.State.Cooks.Add(id);
        await state.WriteStateAsync();
        var cookGrain = GrainFactory.GetGrain<ICookGrain>(id);
        var result = await cookGrain.Initialize(request);
        return result;
    }
}