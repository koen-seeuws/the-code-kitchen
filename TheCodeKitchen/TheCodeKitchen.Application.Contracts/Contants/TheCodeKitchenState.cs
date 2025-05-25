namespace TheCodeKitchen.Application.Contracts.Contants;

public static class TheCodeKitchenState
{
    public static readonly string[] All =
    [
        Cook,
        Equipment,
        Game,
        GameManagement,
        Kitchen,
        KitchenManagement,
        KitchenOrder,
        Order,
        PubSubStore
    ];

    // Grains
    public const string Cook = "TheCodeKitchenCookState";
    public const string Equipment = "TheCodeKitchenEquipmentState";
    public const string Game = "TheCodeKitchenGameState";
    public const string GameManagement = "TheCodeKitchenGameManagementState";
    public const string Kitchen = "TheCodeKitchenKitchenState";
    public const string KitchenManagement = "TheCodeKitchenKitchenManagementState";
    public const string KitchenOrder = "TheCodeKitchenKitchenOrderState";
    public const string Order = "TheCodeKitchenOrderState";

    //Streams
    public const string PubSubStore = nameof(PubSubStore);
}