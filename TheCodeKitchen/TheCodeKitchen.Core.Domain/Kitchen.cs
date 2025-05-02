namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen : DomainEntity, IHasGuidId
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string? Code { get; private set; }

    public Guid Game { get; private set; }

    public ICollection<Guid> Cooks { get; set; }
    

    public Kitchen(Guid id, string name, string code, Guid game)
    {
        Id = id;
        Name = name;
        Code = code;
        Game = game;
        Cooks = [];
    }
}