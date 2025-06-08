using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Requests.Game;
using TheCodeKitchen.Application.Contracts.Response.Game;

namespace TheCodeKitchen.Application.Business.Grains.GameManagementGrain;

public sealed partial class GameManagementGrain
{
    public async Task<Result<CreateGameResponse>> CreateGame(CreateGameRequest request)
    {
        var id = Guid.CreateVersion7();
        state.State.Games.Add(id);
        await state.WriteStateAsync();
        var gameGrain = GrainFactory.GetGrain<IGameGrain>(id);
        var result = await gameGrain.Initialize(request, state.State.Games.Count);
        return result;
    }
}