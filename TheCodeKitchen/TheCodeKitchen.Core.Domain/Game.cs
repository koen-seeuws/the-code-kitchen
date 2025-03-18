namespace TheCodeKitchen.Core.Domain;

public partial class Game : DomainEntity, IHasGuidId
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Paused { get; private set; }
    public ICollection<Kitchen> Kitchens { get; private set; } = [];

    private Game() { }
    
    public Game(string name)
    {
        Id = Guid.CreateVersion7();
        Name = name;
    }
}