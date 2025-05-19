using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");

        var getCooksResult = await GetCooks(new GetCookRequest(request.Username));

        if (!getCooksResult.Succeeded)
            return getCooksResult.Error;

        var foundCook = getCooksResult.Value.FirstOrDefault();
        if (foundCook is not null)
            return new JoinKitchenResponse(
                foundCook.Id,
                foundCook.Username,
                this.GetPrimaryKey(),
                foundCook.PasswordHash,
                false
            );

        var createCookResult =
            await CreateCook(new CreateCookRequest(request.Username, request.PasswordHash, this.GetPrimaryKey()));
        if (!createCookResult.Succeeded)
            return createCookResult.Error;

        var createdCook = createCookResult.Value;
        return new JoinKitchenResponse(
            createdCook.Id,
            createdCook.Username,
            this.GetPrimaryKey(),
            request.PasswordHash,
            true
        );
    }
}