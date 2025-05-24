using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The kitchen with id {this.GetPrimaryKey()} has not been initialized");

        var gameGrain = GrainFactory.GetGrain<IGameGrain>(state.State.Game);
        
        var getCooksResult = await GetCooks(new GetCookRequest(request.Username));

        if (!getCooksResult.Succeeded)
            return getCooksResult.Error;

        var foundCook = getCooksResult.Value.FirstOrDefault();
        
        if (foundCook is not null)
            return new JoinKitchenResponse(state.State.Game,
                state.State.Id,
                foundCook.Id,
                foundCook.Username,
                foundCook.PasswordHash,
                false
            );
        
        var game = await gameGrain.GetGame();

        if (!game.Succeeded)
            return game.Error;
        
        if (game.Value.Started is not null)
            return new GameAlreadyStartedError(
                $"The game with id {state.State.Game} has already started, you can't join a game that has already started!");

        var createCookResult =
            await CreateCook(new CreateCookRequest(request.Username, request.PasswordHash, this.GetPrimaryKey()));
        
        if (!createCookResult.Succeeded)
            return createCookResult.Error;

        var createdCook = createCookResult.Value;
        
        return new JoinKitchenResponse(
            state.State.Game,
            state.State.Id,
            createdCook.Id,
            createdCook.Username,
            request.PasswordHash,
            true
        );
    }
}