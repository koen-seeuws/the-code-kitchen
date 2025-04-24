using TheCodeKitchen.Core.Domain.Events;
using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public void StartGame()
    {
        foreach (var kitchen in Kitchens)
        {
            kitchen.RemoveJoinCode();
        }
        
        if (Started is not null)
            throw new GameAlreadyStartedException($"The game with id {Id} has already started");

        Started = DateTimeOffset.UtcNow;
        Paused = DateTimeOffset.UtcNow;
        
        RaiseEvent(new GameStartedEvent(Id));
    }
}