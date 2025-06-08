namespace TheCodeKitchen.Core.Domain;

public class Ingredient
{
    public string Name { get; set; }
    
    public Ingredient(string name)
    {
        Name = name;
    }
}