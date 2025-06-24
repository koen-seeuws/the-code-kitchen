using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Core.Domain;

public class Food
{
    public Food(Guid id, string name, double temperature, Guid kitchen)
    {
        Id = id;
        Name = name;
        Temperature = temperature;
        Kitchen = kitchen;
        Ingredients = new List<Food>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Temperature { get; set; }
    public TimeSpan TimeOnFurnace { get; set; }
    public TimeSpan TimeOnCuttingBoard { get; set; }
    public TimeSpan TimeInBlender { get; set; }
    public ICollection<Food> Ingredients { get; set; }
    
    public Guid Kitchen { get; set; }

    public Guid? Cook { get; set; }
    public EquipmentType? CurrentEquipmentType { get; set; }
    public int? CurrentEquipmentNumber { get; set; }

    public long? OrderNumber { get; set; }
    
}