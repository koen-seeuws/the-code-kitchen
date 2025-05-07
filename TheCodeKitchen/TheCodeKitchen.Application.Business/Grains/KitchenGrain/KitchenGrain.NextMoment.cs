using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Requests;

namespace TheCodeKitchen.Application.Business.Grains.KitchenGrain;

public partial class KitchenGrain
{
    public async Task<Result<TheCodeKitchenUnit>> NextMoment(NextKitchenMomentRequest request)
    {
        var kitchenId = this.GetPrimaryKey();
        var nextMoment = new NextMomentEvent(kitchenId, request.Moment);

        var stream = streamProvider.GetStream<NextMomentEvent>(nameof(NextMomentEvent), kitchenId);
        await stream.OnNextAsync(nextMoment);
        
        return TheCodeKitchenUnit.Value;
    }
}