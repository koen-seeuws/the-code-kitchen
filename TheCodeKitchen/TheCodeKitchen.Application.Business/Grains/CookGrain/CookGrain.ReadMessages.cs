using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public Task<Result<IEnumerable<ReadMessageResponse>>> ReadMessages()
    {
        Result<IEnumerable<ReadMessageResponse>> result = state.RecordExists
            ? mapper.Map<List<ReadMessageResponse>>(state.State.Messages)
            : new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        return Task.FromResult(result);
    }
}