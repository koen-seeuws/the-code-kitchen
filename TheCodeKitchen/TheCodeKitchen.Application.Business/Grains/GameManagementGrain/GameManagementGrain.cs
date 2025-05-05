namespace TheCodeKitchen.Application.Business.Grains.GameManagementGrain;

public class GameManagementState
{
    public ICollection<Guid> Games { get; } = new List<Guid>();
}

public partial class GameManagementGrain(
    [PersistentState(Storage.GameManagement)]
    IPersistentState<GameManagementState> state
) : Grain, IGameManagementGrain;