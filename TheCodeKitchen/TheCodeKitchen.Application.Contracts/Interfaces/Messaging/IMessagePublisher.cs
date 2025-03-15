namespace TheCodeKitchen.Application.Contracts.Interfaces.Messaging;

public interface IMessagePublisher
{
    Task<long?> PublishAsync<T>(T message, string queueOrTopic, IDictionary<string, object>? properties = null, DateTimeOffset? delay = null, CancellationToken cancellationToken = default);
}