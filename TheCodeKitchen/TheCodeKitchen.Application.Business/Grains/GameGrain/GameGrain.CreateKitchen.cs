using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public sealed partial class GameGrain
{
    public async Task<Result<CreateKitchenResponse>> CreateKitchen(CreateKitchenRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");
        
        if(state.State.Started is not null)
            return new GameAlreadyStartedError($"The game with id {this.GetPrimaryKey()} has already started, you can't add any new kitchens");

        var id = Guid.CreateVersion7();
        state.State.Kitchens.Add(id);
        await state.WriteStateAsync();
        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(id);
        var result = await kitchenGrain.Initialize(request, state.State.Kitchens.Count);
        return result;
    }
}