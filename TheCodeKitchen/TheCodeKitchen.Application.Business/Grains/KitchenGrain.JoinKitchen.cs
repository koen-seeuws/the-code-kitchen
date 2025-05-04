using Orleans;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenGrain
{
    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        var getCooksResult = await GetCooks(new GetCookRequest(request.Username));

        if (!getCooksResult.Succeeded)
            return getCooksResult.Error;

        var foundCook = getCooksResult.Value.FirstOrDefault();
        if (foundCook is not null)
        {
            if (foundCook.PasswordHash != request.Password)
                return new JoinKitchenResponse(foundCook.Id, foundCook.Username, this.GetPrimaryKey());
            else
                return new UnauthorizedError("The password is incorrect");
        }

        var createCookResult =
            await CreateCook(new CreateCookRequest(request.Username, request.Password, this.GetPrimaryKey()));
        if (!createCookResult.Succeeded)
            return createCookResult.Error;

        var createdCook = createCookResult.Value;
        return new JoinKitchenResponse(createdCook.Id, createdCook.Username, this.GetPrimaryKey());
    }
}