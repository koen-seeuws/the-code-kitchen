using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public class CookGrain(
    [PersistentState("Cook")] IPersistentState<Cook> state,
    IMapper mapper
) : Grain, ICookGrain
{
    public async Task<Result<CreateCookResponse>> AddCook(CreateCookRequest request)
    {
        var id = this.GetPrimaryKey();
        var username = request.Username.Trim();

        var cook = new Cook(id, username, request.PasswordHash, request.KitchenId);
        state.State = cook;
        await state.WriteStateAsync();

        return mapper.Map<CreateCookResponse>(cook);
    }
}