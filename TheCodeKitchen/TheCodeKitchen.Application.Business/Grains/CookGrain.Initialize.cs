using Orleans;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class CookGrain
{
    public async Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request)
    {
        var id = this.GetPrimaryKey();
        var username = request.Username.Trim();

        var cook = new Cook(id, username, request.PasswordHash, request.KitchenId);
        state.State = cook;
        await state.WriteStateAsync();

        return mapper.Map<CreateCookResponse>(cook);
    }
}