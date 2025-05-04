using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameManagementGrain
{
    public async Task<Result<IEnumerable<GetGameResponse>>> GetGames()
    {
        var tasks = state.State.Games.Select(async id =>
        {
            var gameGrain = GrainFactory.GetGrain<IGameGrain>(id);
            var result = await gameGrain.GetGame();
            return result;
        });

        var results = await Task.WhenAll(tasks);

        return Result<GetGameResponse>.Combine(results);
    }
}