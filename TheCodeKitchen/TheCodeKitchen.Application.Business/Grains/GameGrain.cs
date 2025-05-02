using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Exceptions;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Interfaces.Authentication;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Application.Business.Grains;

public class GameGrain(
    [PersistentState("Game")] IPersistentState<Game> state,
    IMapper mapper,
    IPasswordHashingService passwordHashingService
) : Grain, IGameGrain
{
    public async Task<Result<CreateGameResponse>> Initialize(CreateGameRequest request, int count)
    {
        var id = this.GetPrimaryKey();
        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
            name = $"Game {count}";

        var game = new Game(id, name);
        state.State = game;
        await state.WriteStateAsync();

        return mapper.Map<CreateGameResponse>(game);
    }

    public async Task<Result<CreateKitchenResponse>> AddKitchen(CreateKitchenRequest request)
    {
        var id = Guid.CreateVersion7();
        state.State.Kitchens.Add(id);
        await state.WriteStateAsync();
        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(id);
        var result = await kitchenGrain.Initialize(request, state.State.Kitchens.Count );
        return result;
    }

    public Task<Result<JoinGameResponse>> JoinGame(JoinGameRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TheCodeKitchenUnit>> StartGame()
    {
        if (state.State.Started is not null)
            return new GameAlreadyStartedException($"The game with id {this.GetPrimaryKey()} has already started");

        state.State.Started = DateTimeOffset.UtcNow;
        state.State.Paused = DateTimeOffset.UtcNow;

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }

    public async Task<Result<PauseOrUnpauseGameResponse>> PauseOrUnpauseGame()
    {
        if (state.State.Paused.HasValue)
            state.State.Paused = null;
        else
            state.State.Paused = DateTimeOffset.UtcNow;

        await state.WriteStateAsync();

        return mapper.Map<PauseOrUnpauseGameResponse>(state.State);
    }

    public Task<Result<GetGameResponse>> GetGame()
        => Task.FromResult<Result<GetGameResponse>>(mapper.Map<GetGameResponse>(state.State));

    public async Task<Result<IEnumerable<GetKitchenResponse>>> GetKitchens()
    {
        var tasks = state.State.Kitchens.Select(async id =>
        {
            var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(id);
            var result = await kitchenGrain.GetKitchen();
            return result;
        });

        var results = await Task.WhenAll(tasks);

        return Result<GetKitchenResponse>.Combine(results);
        
    }
}