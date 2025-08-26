namespace TheCodeKitchen.Core.Domain;

public class Recipe(string name, List<RecipeIngredient>? ingredients, List<RecipeStep>? steps)
{

    public string Name { get; set; } = name;
    public List<RecipeStep> Steps { get; set; } = steps ?? new List<RecipeStep>();
    public List<RecipeIngredient> Ingredients { get; set; } = ingredients ?? new List<RecipeIngredient>();
}