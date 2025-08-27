namespace TheCodeKitchen.Application.Constants;

public static class TheCodeKitchenState
{
    public static readonly string[] Blobs =
    [
        //Grains
        CookBook,
        Cooks,
        Equipment,
        Food,
        Games,
        GameManagement,
        Kitchens,
        KitchenManagement,
        KitchenOrders,
        Orders,
        Pantry,
        
        // Reminders
        Reminders,
        
        // Persistent Streaming
        PubSubStore,
        EventHubCheckpoints,
        StreamHandles
    ];
    
    public static readonly string[] Tables =
    [
        // Reminders
        Reminders,
        
        // Persistent Streaming
        PubSubStore,
        EventHubCheckpoints,
        StreamHandles
    ];

    // Grains
    public const string CookBook = "TheCodeKitchenCookBookState";
    public const string Cooks = "TheCodeKitchenCookState";
    public const string Equipment = "TheCodeKitchenEquipmentState";
    public const string Food = "TheCodeKitchenFoodState";
    public const string Games = "TheCodeKitchenGameState";
    public const string GameManagement = "TheCodeKitchenGameManagementState";
    public const string Kitchens = "TheCodeKitchenKitchenState";
    public const string KitchenManagement = "TheCodeKitchenKitchenManagementState";
    public const string KitchenOrders = "TheCodeKitchenKitchenOrderState";
    public const string Orders = "TheCodeKitchenOrderState";
    public const string Pantry = "TheCodeKitchenPantryState";
    
    // Clustering
    public const string Clustering = "TheCodeKitchenClustering";

    // Reminders
    public const string Reminders = "TheCodeKitchenReminders";
    
    // Persistent Streaming
    public const string PubSubStore = nameof(PubSubStore);
    public const string EventHubCheckpoints = "TheCodeKitchenEventHubCheckpoints";
    public const string StreamHandles = "TheCodeKitchenStreamHandleState";
    
}