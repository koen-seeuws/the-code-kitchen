using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using TheCodeKitchen.Application.Contracts.Interfaces.Queueing;

namespace TheCodeKitchen.Infrastructure.AzureServiceBus;

internal sealed class AzureServiceBusMessagePublisher(ServiceBusClient client) : IMessagePublisher
{
    public async Task<long?> PublishAsync<T>(T message, string queueOrTopic,
        IDictionary<string, object>? properties = null, DateTimeOffset? delay = null,
        CancellationToken cancellationToken = default)
    {
        var sender = client.CreateSender(queueOrTopic);

        ServiceBusMessage serviceBusMessage;
        switch (message)
        {
            case string stringMessage:
                serviceBusMessage = new ServiceBusMessage(stringMessage);
                break;
            case byte[] byteArrayMessage:
                serviceBusMessage = new ServiceBusMessage(byteArrayMessage);
                break;
            default:
                var jsonMessage = JsonSerializer.Serialize(message);
                serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    ContentType = "application/json"
                };
                break;
        }

        serviceBusMessage.ApplicationProperties.Add("Type", message?.GetType().Name);

        if (properties is not null)
        {
            foreach (var (key, value) in properties)
            {
                serviceBusMessage.ApplicationProperties.TryAdd(key, value);
            }
        }

        if (delay.HasValue)
            return await sender.ScheduleMessageAsync(serviceBusMessage, delay.Value, cancellationToken);

        await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
        return null;
    }
}