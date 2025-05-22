namespace TheCodeKitchen.Application.Business.Grains.GameManagementGrain;

public class GameManagementState
{
    public ICollection<Guid> Games { get; } = new List<Guid>();
}

public sealed partial class GameManagementGrain(
    [PersistentState(TheCodeKitchenStorage.GameManagement, TheCodeKitchenStorage.GameManagement)]
    IPersistentState<GameManagementState> state
) : Grain, IGameManagementGrain;