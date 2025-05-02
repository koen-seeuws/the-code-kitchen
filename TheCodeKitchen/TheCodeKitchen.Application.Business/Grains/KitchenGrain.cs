using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public class KitchenGrain(
    [PersistentState("Kitchen")] IPersistentState<Kitchen> state,
    IMapper mapper
) : Grain, IKitchenGrain
{
    public async Task<Result<CreateKitchenResponse>> Initialize(CreateKitchenRequest request, int count)
    {
        var id = this.GetPrimaryKey();

        var name = request.Name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Kitchen {count}";

        var codeGenerationGrain = GrainFactory.GetGrain<ICodeGenerationGrain>(Guid.Empty);
        var code = await codeGenerationGrain.GenerateUniqueCode();

        var kitchen = new Kitchen(id, name, code, request.GameId);

        state.State = kitchen;
        await state.WriteStateAsync();

        return mapper.Map<CreateKitchenResponse>(kitchen);
    }

    public Task<Result<GetKitchenResponse>> GetKitchen()
        => Task.FromResult<Result<GetKitchenResponse>>(mapper.Map<GetKitchenResponse>(state.State));
}