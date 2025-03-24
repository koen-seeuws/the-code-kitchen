namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen : DomainEntity, IHasGuidId
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string? Code { get; private set; }

    public Guid GameId { get; private set; }
    public Game Game { get; private set; }

    public ICollection<Cook> Cooks { get; set; }

    private Kitchen()
    {
    }

    public Kitchen(string name, string code, Game game)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Code = code;
        GameId = game.Id;
        Game = game;
        Cooks = [];
    }
}