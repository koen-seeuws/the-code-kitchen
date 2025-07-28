namespace TheCodeKitchen.Application.Contracts.Constants;

public static class TheCodeKitchenStreams
{
    public const string DefaultTheCodeKitchenProvider = "Default";

    public static readonly List<string> AzureStorageQueues =
        Enumerable.Range(1, 10)
            .Select(i => $"the-code-kitchen-streaming-queue-{i}")
            .ToList();
}