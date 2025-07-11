namespace TheCodeKitchen.Application.Business.Extensions;

public static class RecipeExtensions
{
    public static string GetRecipeComboIdentifier(this IEnumerable<string> ingredientNames)
    {
        const char separator = '-'; 
        
        ingredientNames = ingredientNames
            .Select(i => i.Trim().ToCamelCase())
            .Order();
        
        return string.Join(separator, ingredientNames);
    }
}