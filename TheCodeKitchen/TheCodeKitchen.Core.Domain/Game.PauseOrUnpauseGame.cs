using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public void PauseOrUnpauseGame()
    {
        if (Paused.HasValue)
            Paused = null;
        else
            Paused = DateTimeOffset.UtcNow;
        
        RaiseEvent(new GamePausedOrUnpausedEvent(Id, Paused));
    }
}