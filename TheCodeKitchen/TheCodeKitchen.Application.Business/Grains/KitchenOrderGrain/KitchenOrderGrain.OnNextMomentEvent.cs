namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public partial class KitchenOrderGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        state.State.Time += TheCodeKitchenMomentDuration.Value;

        //TODO:
        // Rating down order when it takes too long
        
    }
}