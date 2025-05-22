namespace TheCodeKitchen.Application.Contracts.Contants;

public static class TheCodeKitchenStorage
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
    public const string Cook = nameof(Cook);
    public const string Equipment = nameof(Equipment);
    public const string Game = nameof(Game);
    public const string GameManagement = nameof(GameManagement);
    public const string Kitchen = nameof(Kitchen);
    public const string KitchenManagement = nameof(KitchenManagement);
    public const string KitchenOrder = nameof(KitchenOrder);
    public const string Order = nameof(Order);

    //Streams
    public const string PubSubStore = nameof(PubSubStore);
}