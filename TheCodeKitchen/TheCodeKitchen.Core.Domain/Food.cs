namespace TheCodeKitchen.Core.Domain;

public class Food
{
    public Food(Guid id, string name, double temperature, Guid kitchenId)
    {
        Id = id;
        Name = name;
        Temperature = temperature;
        KitchenId = kitchenId;
        Ingredients = new List<Food>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Temperature { get; set; }
    public TimeSpan TimeOnFurnace { get; set; }
    public TimeSpan TimeOnCuttingBoard { get; set; }
    public TimeSpan TimeInBlender { get; set; }
    public ICollection<Food> Ingredients { get; set; }
    
    public Guid KitchenId { get; set; }
}