using Microsoft.AspNetCore.SignalR;
using Orleans.Streams;
using TheCodeKitchen.Application.Business.Contants;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Presentation.API.Cook.Hubs;

namespace TheCodeKitchen.Presentation.API.Cook.StreamSubscribers;

public class NextMomentStreamSubscriber(
    IClusterClient clusterClient,
    IHubContext<KitchenHub> hubContext
)
{
    private readonly IDictionary<Guid, StreamSubscriptionHandle<NextMomentEvent>> _subscriptions =
        new Dictionary<Guid, StreamSubscriptionHandle<NextMomentEvent>>();
    
    private readonly IDictionary<Guid, int> _subscriptionCounts =
        new Dictionary<Guid, int>();

    public async Task SubscribeToKitchenEvents(Guid kitchenId)
    {
        if (_subscriptionCounts.TryGetValue(kitchenId, out var count))
        {
            _subscriptionCounts[kitchenId] = count + 1;
        }
        else
        {
            _subscriptionCounts[kitchenId] = 1;
        }
        
        if (!_subscriptions.ContainsKey(kitchenId))
        {
            var streamProvider = clusterClient.GetStreamProvider(TheCodeKitchenStreams.Default);
            var streamId = StreamId.Create(nameof(NextMomentEvent), kitchenId);
            var stream = streamProvider.GetStream<NextMomentEvent>(streamId);
            
            var handle = await stream.SubscribeAsync(async nextMoments =>
            {
                foreach (var nextMoment in nextMoments)
                {
                    await hubContext.Clients.Group(kitchenId.ToString()).SendAsync(nameof(NextMomentEvent), nextMoment);
                }
            });
            
            _subscriptions.Add(kitchenId, handle);
        }
    }
    
    public async Task UnSubscribeFromKitchenEvents(Guid kitchenId)
    {
        if (_subscriptionCounts.TryGetValue(kitchenId, out var count))
        {
            _subscriptionCounts[kitchenId] = count - 1;
            if (_subscriptionCounts[kitchenId] <= 0)
            {
                if (_subscriptions.TryGetValue(kitchenId, out var handle))
                {
                    await handle.UnsubscribeAsync();
                    _subscriptions.Remove(kitchenId);
                }
                _subscriptionCounts.Remove(kitchenId);
            }
        }
    }
}
