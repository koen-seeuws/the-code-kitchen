namespace TheCodeKitchen.Core.Domain;

public class Recipe(string name, ICollection<RecipeIngredient>? ingredients, ICollection<RecipeStep>? steps)
{

    public string Name { get; set; } = name;
    public ICollection<RecipeStep> Steps { get; set; } = steps ?? new List<RecipeStep>();
    public ICollection<RecipeIngredient> Ingredients { get; set; } = ingredients ?? new List<RecipeIngredient>();
}