using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        var getCooksResult = await GetCooks(new GetCookRequest(request.Username));

        if (!getCooksResult.Succeeded)
            return getCooksResult.Error;

        var foundCook = getCooksResult.Value.FirstOrDefault();
        if (foundCook is not null)
            return passwordHashingService.VerifyHashedPassword(foundCook.PasswordHash, request.Password)
                ? new JoinKitchenResponse(foundCook.Id, foundCook.Username, this.GetPrimaryKey())
                : new UnauthorizedError("The password is incorrect");
        
        var passwordHash = passwordHashingService.HashPassword(request.Password);
        var createCookResult =
            await CreateCook(new CreateCookRequest(request.Username, passwordHash, this.GetPrimaryKey()));
        if (!createCookResult.Succeeded)
            return createCookResult.Error;

        var createdCook = createCookResult.Value;
        return new JoinKitchenResponse(createdCook.Id, createdCook.Username, this.GetPrimaryKey());
    }
}