using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class GameGrain
{
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