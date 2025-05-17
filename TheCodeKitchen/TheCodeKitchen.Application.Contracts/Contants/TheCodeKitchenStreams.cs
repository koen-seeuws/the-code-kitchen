namespace TheCodeKitchen.Application.Contracts.Contants;

public static class TheCodeKitchenStreams
{
    public const string AzureStorageQueuesProvider = nameof(AzureStorageQueuesProvider);

    public static readonly List<string> AzureStorageQueues =
        Enumerable.Range(1, 10)
            .Select(i => $"the-code-kitchen-streaming-queue-{i}")
            .ToList();
}