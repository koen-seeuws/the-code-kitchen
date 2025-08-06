namespace TheCodeKitchen.Infrastructure.AzureSignalR.Constants;

public static class GroupConstants
{
    public static string GetKitchenGroup(Guid kitchenId)
    {
        return $"kitchen-{kitchenId}";
    }
}