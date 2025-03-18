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
        var kitchen = new Kitchen(name, code, Id);
        Kitchens.Add(kitchen);
        
        RaiseEvent(new KitchenCreatedEvent(kitchen.Id, kitchen.Name, kitchen.Code, this.Id));
        
        return kitchen;
    }
}