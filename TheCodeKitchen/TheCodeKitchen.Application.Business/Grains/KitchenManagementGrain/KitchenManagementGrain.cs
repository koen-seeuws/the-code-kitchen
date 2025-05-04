using TheCodeKitchen.Application.Contracts.Interfaces.Security;

namespace TheCodeKitchen.Application.Business.Grains.KitchenManagementGrain;

public class KitchenManagementState
{
    public IDictionary<string, Guid> KitchenCodes { get; set; } = new Dictionary<string, Guid>();
}

public partial class KitchenManagementGrain(
    [PersistentState("KitchenCodes")] IPersistentState<KitchenManagementState> state
) : Grain, IKitchenManagementGrain;