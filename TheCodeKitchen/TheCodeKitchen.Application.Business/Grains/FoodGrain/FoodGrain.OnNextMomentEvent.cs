namespace TheCodeKitchen.Application.Business.Grains.FoodGrain;

public partial class FoodGrain
{
    private async Task OnNextMomentEvent(NextMomentEvent nextMomentEvent, StreamSequenceToken _)
    {
        var temperatureTransferRate = 0.02; // 0.01 to 0.03 for food in room
        
        //TODO: food processing logic here
        
        
        //Temperature according to Newton's Law of Cooling
        state.State.Temperature += (nextMomentEvent.Temperature - state.State.Temperature) * temperatureTransferRate;
    }
}