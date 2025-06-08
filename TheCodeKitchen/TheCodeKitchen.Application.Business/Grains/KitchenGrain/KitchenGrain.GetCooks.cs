using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Requests.Cook;
using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public sealed partial class KitchenGrain
{
    public async Task<Result<IEnumerable<GetCookResponse>>> GetCooks(GetCookRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError($"The game with id {this.GetPrimaryKey()} has not been initialized");

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