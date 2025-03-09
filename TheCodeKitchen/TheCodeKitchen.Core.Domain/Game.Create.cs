using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Core.Domain;

public partial class  Game
{
    public static Game Create(long amountOfExistingGames, string? name = null)
    {
        name = name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Game {amountOfExistingGames + 1}";

        var game = new Game(name);
        
        game.RaiseEvent(new GameCreatedEvent(game.Id, game.Name));
        
        return game;
    }
}