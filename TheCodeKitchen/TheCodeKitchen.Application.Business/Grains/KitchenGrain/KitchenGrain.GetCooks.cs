using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<IEnumerable<GetCookResponse>>> GetCooks(GetCookRequest request)
    {
        var tasks = state.State.Cooks.Select(async id =>
        {
            var kitchenGrain = GrainFactory.GetGrain<ICookGrain>(id);
            var result = await kitchenGrain.GetCook();
            return result;
        });

        IEnumerable<Result<GetCookResponse>> results = await Task.WhenAll(tasks);

        if (!results.All(x => x.Succeeded))
            return Result<GetCookResponse>.Combine(results);

        var username = request.Username?.Trim().ToLower();
        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            results = results.Where(x => x.Value.Username.Trim().ToLower() == username);
        }

        return Result<GetCookResponse>.Combine(results);
    }
}