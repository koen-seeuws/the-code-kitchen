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
) : Grain, IGameManagementGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new GameManagementState();
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }
}