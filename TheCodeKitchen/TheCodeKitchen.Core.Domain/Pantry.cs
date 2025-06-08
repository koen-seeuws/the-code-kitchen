namespace TheCodeKitchen.Core.Domain;

public class Pantry(Guid id, double temperature)
{
    public Guid Id { get; } = id;
    public double Temperature { get; set; } = temperature;
    public ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
}