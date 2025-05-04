using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public class GameManagementState
{
    public ICollection<Guid> Games { get; } = new List<Guid>();
}

public partial class GameManagementGrain(
    [PersistentState("Games")] IPersistentState<GameManagementState> state
) : Grain, IGameManagementGrain;