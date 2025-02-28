namespace TheCodeKitchen.Server.Application.Contracts.Interfaces.Queueing;

public interface IMessagePublisher
{
    Task<long?> PublishAsync<T>(T message, string queueOrTopic, IDictionary<string, object>? properties = null, DateTimeOffset? delay = null, CancellationToken cancellationToken = default);
}