namespace TheCodeKitchen.Application.Business.Grains.KitchenManagementGrain;

public class KitchenManagementState
{
    public IDictionary<string, Guid> KitchenCodes { get; set; } = new Dictionary<string, Guid>();
}

public sealed partial class KitchenManagementGrain(
    [PersistentState(TheCodeKitchenStorage.KitchenManagement, TheCodeKitchenStorage.KitchenManagement)]
    IPersistentState<KitchenManagementState> state
) : Grain, IKitchenManagementGrain;