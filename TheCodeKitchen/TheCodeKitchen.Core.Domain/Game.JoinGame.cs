

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public Cook JoinGame(string joinCode, string username, string passwordHash)
    {
        /*
        var cook = Kitchens
            .SelectMany(kitchen => kitchen.Cooks)
            .FirstOrDefault(cook => string.Equals(cook.Username, username, StringComparison.CurrentCultureIgnoreCase));
        
        if (cook != null)
            throw new AlreadyJoinedException($"There is already a cook with the username {username} in game {Id}");
        
        if(Started != null)
            throw new GameAlreadyStartedException($"Game {Id} has already started. You cannot join a game that has already started.");
           
        var kitchen = Kitchens.FirstOrDefault(kitchen => kitchen.Code == joinCode);
        if (kitchen == null) 
            throw new InvalidJoinCodeException($"No kitchen with code {joinCode} was found");
        
        cook = kitchen.AddCook(username, passwordHash);
        
        RaiseEvent(new CookJoinedEvent(cook.Id, cook.Username, Id, kitchen.Id));
        
        return cook;
        */
        return new Cook(Guid.Empty, "", "", Guid.Empty);
    }
}