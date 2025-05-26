namespace TheCodeKitchen.Application.Contracts.Contants;

public static class TheCodeKitchenState
{
    public static readonly string[] All =
    [
        //Grains
        Cooks,
        Equipment,
        Games,
        GameManagement,
        Kitchens,
        KitchenManagement,
        KitchenOrders,
        Orders,
        
        // Persistent Streaming
        PubSubStore,
        StreamHandles
    ];

    // Grains
    public const string Cooks = "TheCodeKitchenCookState";
    public const string Equipment = "TheCodeKitchenEquipmentState";
    public const string Games = "TheCodeKitchenGameState";
    public const string GameManagement = "TheCodeKitchenGameManagementState";
    public const string Kitchens = "TheCodeKitchenKitchenState";
    public const string KitchenManagement = "TheCodeKitchenKitchenManagementState";
    public const string KitchenOrders = "TheCodeKitchenKitchenOrderState";
    public const string Orders = "TheCodeKitchenOrderState";

    // Persistent Streaming
    public const string PubSubStore = nameof(PubSubStore);
    public const string StreamHandles = "TheCodeKitchenStreamHandleState";
}