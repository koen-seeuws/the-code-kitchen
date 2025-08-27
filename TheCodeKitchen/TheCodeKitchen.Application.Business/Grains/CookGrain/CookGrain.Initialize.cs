using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<CreateCookResponse>> Initialize(CreateCookRequest request)
    {
        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Initializing cook...",
            this.GetPrimaryKey(), this.GetPrimaryKeyString());

        if (state.RecordExists)
        {
            logger.LogWarning("Kitchen {Kitchen} - Cook {Username}: Cook already initialized",
                this.GetPrimaryKey(), this.GetPrimaryKeyString());
            return new AlreadyExistsError(
                $"The cook with username {this.GetPrimaryKeyString()} has already been initialized in kitchen {this.GetPrimaryKey()}");
        }

        var username = request.Username.Trim();

        var cook = new Cook(username, request.PasswordHash, request.Game, request.Kitchen);
        state.State = cook;
        await state.WriteStateAsync();

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Cook successfully initialized", state.State.Kitchen,
            state.State.Username);
        await SubscribeToNextMomentEvent();

        return mapper.Map<CreateCookResponse>(cook);
    }
}