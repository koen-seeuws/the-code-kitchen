using TheCodeKitchen.Core.Domain.Events;
using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public Cook JoinGame(string joinCode, string username, string passwordHash)
    {
        var cook = Kitchens
            .SelectMany(kitchen => kitchen.Cooks)
            .FirstOrDefault(cook => string.Equals(cook.Username, username, StringComparison.CurrentCultureIgnoreCase));

        if (cook != null) return cook;
           
        var kitchen = Kitchens.FirstOrDefault(kitchen => kitchen.Code == joinCode);
        if (kitchen == null) 
            throw new InvalidJoinCodeException($"No kitchen with code {joinCode} was found");

        cook = kitchen.AddCook(username, passwordHash);
        
        RaiseEvent(new CookJoinedEvent(cook.Id, cook.Username, Id, kitchen.Id));
        
        return cook;
    }
}