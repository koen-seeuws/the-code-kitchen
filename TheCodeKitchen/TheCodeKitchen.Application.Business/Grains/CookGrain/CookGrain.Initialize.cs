using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request)
    {
        if(state.RecordExists)
            return new AlreadyExistsError($"The cook with id {this.GetPrimaryKey()} has already been initialized");
        
        var id = this.GetPrimaryKey();
        var username = request.Username.Trim();

        var cook = new Cook(id, username, request.PasswordHash, request.KitchenId);
        state.State = cook;
        await state.WriteStateAsync();

        return mapper.Map<CreateCookResponse>(cook);
    }
}