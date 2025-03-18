namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen : DomainEntity, IHasGuidId
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string? Code { get; private set; }
    
    public Guid GameId { get; private set; }
    public Game Game { get; private set; } = null!;

    private Kitchen() { }
    
    public Kitchen(string name, string code, Guid gameId)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Code = code;
        GameId = gameId;
    }
}