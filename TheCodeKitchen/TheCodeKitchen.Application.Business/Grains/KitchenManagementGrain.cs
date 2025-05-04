using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public class KitchenCodeIndexState
{
    public IDictionary<string, Guid> KitchenCodes { get; set; } = new Dictionary<string, Guid>();
}

public partial class KitchenManagementGrain(
    [PersistentState("KitchenCodes")] IPersistentState<KitchenCodeIndexState> state
) : Grain, IKitchenManagementGrain;
