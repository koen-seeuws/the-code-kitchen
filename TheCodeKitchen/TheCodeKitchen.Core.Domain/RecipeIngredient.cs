namespace TheCodeKitchen.Core.Domain;

public class RecipeIngredient(string name, ICollection<RecipeStep> steps)
{
    public string Name { get; set; } = name;
    public ICollection<RecipeStep> Steps { get; set; } = steps;
}