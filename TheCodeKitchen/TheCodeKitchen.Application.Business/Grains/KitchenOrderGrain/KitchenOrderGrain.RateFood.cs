using TheCodeKitchen.Application.Constants;
using TheCodeKitchen.Application.Contracts.Models.Food;
using TheCodeKitchen.Application.Contracts.Models.Recipe;
using TheCodeKitchen.Application.Contracts.Response.CookBook;

namespace TheCodeKitchen.Application.Business.Grains.KitchenOrderGrain;

public sealed partial class KitchenOrderGrain
{
    private double RateFood(string food, ICollection<RecipeStep> executedSteps, ICollection<Food> ingredients,
        ICollection<GetRecipeResponse> recipes)
    {
        var recipe = recipes.FirstOrDefault(r => r.Name.Equals(food, StringComparison.OrdinalIgnoreCase));

        if (recipe is null)
            return 0.0; // If someone tries to rate a food that isn't a recipe, rate 0

        var recipeRating = RateSteps(executedSteps, recipe.Steps);

        // Create lookup: name -> list of recipe ingredients with that name
        var recipeIngredientGroups = recipe.Ingredients
            .GroupBy(i => i.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

        // Track occurrences of each ingredient name as we loop
        var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        var ingredientRatings = ingredients.Select(f =>
        {
            var name = f.Name;

            // Track how many times we've seen this ingredient name
            nameCounts.TryAdd(name, 0);

            var occurrence = nameCounts[name];
            nameCounts[name]++;

            // Try to get the matching recipe ingredient by occurrence
            var recipeMatch = recipeIngredientGroups.TryGetValue(name, out var matches) && occurrence < matches.Count
                ? matches[occurrence]
                : null;

            var isRecipe = recipes.Any(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return isRecipe
                ? RateFood(name, f.Steps, f.Ingredients, recipes)
                : RateSteps(f.Steps, recipeMatch?.Steps ?? new List<RecipeStepDto>());
        }).ToList();

        ingredientRatings.Add(recipeRating);
        return ingredientRatings.Average();
    }

    private double RateSteps(ICollection<RecipeStep> executedSteps, ICollection<RecipeStepDto> expectedSteps)
    {
        var executed = executedSteps.ToList();
        var expected = expectedSteps.ToList();

        // Handle edge case: no expected steps
        if (expected.Count == 0)
        {
            return executed.Count == 0 ? 1.0 : 0.0;
        }

        var matched = new HashSet<int>();
        var totalScore = 0.0;

        foreach (var actual in executed)
        {
            var bestScore = 0.0;
            var bestIndex = -1;

            for (var i = 0; i < expected.Count; i++)
            {
                if (matched.Contains(i)) continue;

                var expectedStep = expected[i];
                var score = 1.0;

                // Equipment match
                if (!actual.EquipmentType.Equals(expectedStep.EquipmentType, StringComparison.OrdinalIgnoreCase))
                    score = 0.0;

                // Time match (within 10% margin)
                var expectedSeconds = expectedStep.Time.TotalSeconds;
                var actualSeconds = actual.Time.TotalSeconds;
                var margin = expectedSeconds * TheCodeKitchenStepTimeMargin.Value;

                if (Math.Abs(expectedSeconds - actualSeconds) > margin)
                    score *= 0.5;

                if (!(score > bestScore)) continue;

                bestScore = score;
                bestIndex = i;
            }

            if (bestIndex != -1)
            {
                matched.Add(bestIndex);
                totalScore += bestScore;
            }
            else
            {
                // No match found — penalty
                totalScore += 0.0;
            }
        }

        // Penalty for missing expected steps
        var missingSteps = expected.Count - matched.Count;
        totalScore += missingSteps * 0.0;

        return totalScore / expected.Count;
    }
}