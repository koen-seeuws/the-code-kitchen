using TheCodeKitchen.Core.Domain.Abstractions;

namespace TheCodeKitchen.Core.Domain;

public partial class Game : DomainObject
{
    public long Id { get; init; }
    public string Name { get; private set; }
    public DateTimeOffset? Paused { get; private set; }
    public ICollection<Kitchen> Kitchens { get; private set; } = [];
    
    private Game() { }
    
    public Game(string name)
    {
        Name = name;
    }
}