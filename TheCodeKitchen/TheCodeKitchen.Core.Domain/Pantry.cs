namespace TheCodeKitchen.Core.Domain;

public class Pantry(Guid id, double temperature)
{
    public Guid Id { get; set; } = id;
    public double Temperature { get; set; } = temperature;
    public ICollection<PantryIngredient> Ingredients { get; set; } = new List<PantryIngredient>();
}