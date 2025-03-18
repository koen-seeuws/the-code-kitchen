using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public void StartGame()
    {
        if (Started is not null)
            throw new GameAlreadyStartedException($"The game with id {Id} has already started");

        foreach (var kitchen in Kitchens)
        {
            kitchen.CloseRegistrations();
        }

        Started = DateTimeOffset.UtcNow;
        Paused = DateTimeOffset.Now;
    }
}