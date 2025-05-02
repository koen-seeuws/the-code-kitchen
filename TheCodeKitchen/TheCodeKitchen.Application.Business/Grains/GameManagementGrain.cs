using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public class GameManagementState
{
    public ICollection<Guid> Games { get; } = new List<Guid>();
}

public class GameManagementGrain(
    [PersistentState("Games")] IPersistentState<GameManagementState> state
) : Grain, IGameManagementGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new GameManagementState();
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public async Task<Result<CreateGameResponse>> CreateGame(CreateGameRequest request)
    {
        var id = Guid.CreateVersion7();
        state.State.Games.Add(id);
        await state.WriteStateAsync();
        var gameGrain = GrainFactory.GetGrain<IGameGrain>(id);
        return await gameGrain.Initialize(request, state.State.Games.Count);
    }

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