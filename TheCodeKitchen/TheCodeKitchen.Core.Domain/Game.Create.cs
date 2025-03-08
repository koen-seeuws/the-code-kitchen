namespace TheCodeKitchen.Core.Domain;

public partial record Game
{
    public static Game Create(long amountOfExistingGames, string? name = null)
    {
        name = name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Game {amountOfExistingGames + 1}";
        
        return new Game(null, name, null,[], []);
    }
}