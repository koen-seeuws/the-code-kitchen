using Orleans.Streams;
using TheCodeKitchen.Application.Contracts.Contants;

namespace TheCodeKitchen.Presentation;

public class StreamSubscriber<TId, TEvent>(IClusterClient clusterClient)
    where TId : IEquatable<TId>
    where TEvent : class
{
    private readonly Dictionary<(TId, string), (StreamSubscriptionHandle<TEvent> Handle, int Count)> _subscriptions =
        new();

    public async Task SubscribeToEvents(TId id, Func<TEvent, Task> onNext, string? streamNamespace = null)
    {
        streamNamespace ??= typeof(TEvent).Name;
        var key = (id, streamNamespace);

        if (_subscriptions.TryGetValue(key, out var subscription))
        {
            _subscriptions[key] = (subscription.Handle, subscription.Count + 1);
            return;
        }

        var streamProvider = clusterClient.GetStreamProvider(TheCodeKitchenStreams.DefaultTheCodeKitchenProvider);
        var streamId = CreateStreamId(streamNamespace, id);
        var stream = streamProvider.GetStream<TEvent>(streamId);

        var handle = await stream.SubscribeAsync(events =>
            Task.WhenAll(events.Select(@event => onNext(@event.Item)))
        );

        _subscriptions[key] = (handle, 1);
    }

    public async Task UnsubscribeFromEvents(TId id, string? streamNamespace = null)
    {
        streamNamespace ??= typeof(TEvent).Name;
        var key = (id, streamNamespace);

        if (!_subscriptions.TryGetValue(key, out var subscription))
            return;

        if (subscription.Count <= 1)
        {
            await subscription.Handle.UnsubscribeAsync();
            _subscriptions.Remove(key);
        }
        else
        {
            _subscriptions[key] = (subscription.Handle, subscription.Count - 1);
        }
    }

    private static StreamId CreateStreamId(string streamNamespace, TId id) =>
        id switch
        {
            string @string => StreamId.Create(streamNamespace, @string),
            Guid guid => StreamId.Create(streamNamespace, @guid),
            long @long => StreamId.Create(streamNamespace, @long),
            _ => throw new NotSupportedException($"Unsupported stream ID type: {typeof(TId)}")
        };
}