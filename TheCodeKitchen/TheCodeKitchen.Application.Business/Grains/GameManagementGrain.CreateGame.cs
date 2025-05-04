using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameManagementGrain
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