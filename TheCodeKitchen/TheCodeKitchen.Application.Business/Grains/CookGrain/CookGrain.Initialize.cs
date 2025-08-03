using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request)
    {
        if(state.RecordExists)
            return new AlreadyExistsError($"The cook with username {this.GetPrimaryKeyString()} has already been initialized in kitchen {this.GetPrimaryKey()}");
        
        var id = this.GetPrimaryKeyString();
        var username = request.Username.Trim();

        var cook = new Cook(username, request.PasswordHash, request.KitchenId);
        state.State = cook;
        await state.WriteStateAsync();

        return mapper.Map<CreateCookResponse>(cook);
    }
}