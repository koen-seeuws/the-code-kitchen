using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    public Kitchen AddKitchen(string? name, string code)
    {
        // Make sure the kitchen has a name
        name = name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Kitchen {Kitchens.Count + 1}";

        // Create a new Kitchen instance
        var kitchen = new Kitchen(name, code, this);
        Kitchens.Add(kitchen);
        
        RaiseEvent(new KitchenAddedEvent(kitchen.Id, kitchen.Name, kitchen.Code, this.Id));
        
        return kitchen;
    }
}