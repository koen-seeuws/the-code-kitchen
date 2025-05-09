using Orleans.Streams;
using TheCodeKitchen.Application.Contracts.Contants;

namespace TheCodeKitchen.Presentation.WebCore;

public class StreamSubscriber<TId, TEvent>(IClusterClient clusterClient)
    where TId : IEquatable<TId>
    where TEvent : class
{
    private readonly IDictionary<TId, StreamSubscriptionHandle<TEvent>> _subscriptions =
        new Dictionary<TId, StreamSubscriptionHandle<TEvent>>();

    private readonly IDictionary<TId, int> _subscriptionCounts =
        new Dictionary<TId, int>();

    public async Task SubscribeToEvents(TId id, Func<TEvent, Task> onNext, string? streamNamespace = null)
    {
        if (_subscriptionCounts.TryGetValue(id, out var count))
            _subscriptionCounts[id] = count + 1;
        else
            _subscriptionCounts[id] = 1;

        if (_subscriptions.ContainsKey(id)) return;

        var streamProvider = clusterClient.GetStreamProvider(TheCodeKitchenStreams.Default);
        streamNamespace ??= typeof(TEvent).Name;
        var streamId = CreateStreamId(streamNamespace, id);
        var stream = streamProvider.GetStream<TEvent>(streamId);

        var handle = await stream.SubscribeAsync(async nextMoments =>
        {
            foreach (var nextMoment in nextMoments)
            {
                await onNext(nextMoment.Item);
            }
        });

        _subscriptions.Add(id, handle);
    }

    public async Task UnsubscribeFromEvents(TId id)
    {
        if (!_subscriptionCounts.TryGetValue(id, out var count)) return;

        if (count <= 1)
        {
            _subscriptionCounts.Remove(id);

            if (_subscriptions.Remove(id, out var handle))
            {
                await handle.UnsubscribeAsync();
            }
        }
        else
        {
            _subscriptionCounts[id] = count - 1;
        }
    }

    private static StreamId CreateStreamId(string streamNamespace, TId id)
    {
        return id switch
        {
            string @string => StreamId.Create(streamNamespace, @string),
            Guid guid => StreamId.Create(streamNamespace, guid),
            long @long => StreamId.Create(streamNamespace, @long),
            _ => throw new NotSupportedException($"Unsupported stream ID type: {typeof(TId)}")
        };
    }
}