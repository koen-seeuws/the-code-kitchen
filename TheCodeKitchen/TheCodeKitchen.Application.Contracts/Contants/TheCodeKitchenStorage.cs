using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Application.Contracts.Contants;

public static class TheCodeKitchenStorage
{
    public static readonly string[] All =
    [
        Cook,
        Game,
        GameManagement,
        Kitchen,
        KitchenManagement,
        PubSubStore
    ];

    // Grains
    public const string Cook = nameof(Cook);
    public const string Game = nameof(Game);
    public const string GameManagement = nameof(GameManagement);
    public const string Kitchen = nameof(Kitchen);
    public const string KitchenManagement = nameof(KitchenManagement);

    //Streams
    public const string PubSubStore = nameof(PubSubStore);
}