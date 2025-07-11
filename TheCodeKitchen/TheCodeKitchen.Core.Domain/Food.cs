namespace TheCodeKitchen.Core.Domain;

public class Food(Guid id, string name, double temperature, Guid kitchen, List<Food>? ingredients = null)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public double Temperature { get; set; } = temperature;
    public ICollection<Food> Ingredients { get; set; } = ingredients ?? new List<Food>();
    public ICollection<RecipeStep> Steps { get; set; } = new List<RecipeStep>();

    public Guid Kitchen { get; set; } = kitchen;

    public Guid? Cook { get; set; }
    public string? CurrentEquipmentType { get; set; }
    public int? CurrentEquipmentNumber { get; set; }

    public long? OrderNumber { get; set; }
    
}