using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain
{
    public async Task<Result<CreateGameResponse>> Initialize(CreateGameRequest request, int count)
    {
        var id = this.GetPrimaryKey();
        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
            name = $"Game {count}";

        var game = new Game(id, name, request.SpeedModifier);
        state.State = game;
        await state.WriteStateAsync();

        return mapper.Map<CreateGameResponse>(game);
    }
}