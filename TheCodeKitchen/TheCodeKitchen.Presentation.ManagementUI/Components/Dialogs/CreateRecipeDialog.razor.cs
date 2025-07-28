using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;
using TheCodeKitchen.Application.Contracts.Response.CookBook;
using TheCodeKitchen.Application.Contracts.Response.Pantry;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

public partial class CreateRecipeDialog(ISnackbar snackbar, IClusterClient clusterClient) : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public ICollection<GetIngredientResponse>? Ingredients { get; set; }

    [Parameter] public ICollection<GetRecipeResponse>? Recipes { get; set; }
    
    private string? ErrorMessage { get; set; }

    private ICollection<string> AllIngredients = new List<string>();

    private MudForm Form { get; set; } = new();

    private CreateRecipeFormModel Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (Recipes == null)
        {
            try
            {
                var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);

                var getRecipesResult = await cookBookGrain.GetRecipes();
                if (getRecipesResult.Succeeded)
                    Recipes = getRecipesResult.Value.ToList();
                else
                    ErrorMessage = getRecipesResult.Error.Message;
            }
            catch
            {
                ErrorMessage = "An error occurred while retrieving the recipe.";
            }
        }

        if (Ingredients == null)
        {
            try
            {
                var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);

                var getIngredientsResult = await pantryGrain.GetIngredients();
                if (getIngredientsResult.Succeeded)
                    Ingredients = getIngredientsResult.Value.ToList();
                else
                    ErrorMessage = getIngredientsResult.Error.Message;
            }
            catch
            {
                ErrorMessage = "An error occurred while retrieving the ingredients.";
            }
        }

        if (Recipes != null && Ingredients != null)
        {
            var recipeNames = Recipes.Select(r => r.Name).ToList();
            var ingredientNames = Ingredients.Select(i => i.Name).ToList();
            AllIngredients = recipeNames.Concat(ingredientNames).ToList();
        } 

        await base.OnInitializedAsync();
    }

    private async Task Submit()
    {
        await Form.Validate();
        if (!Form.IsValid)
            return;

        //TODO
        //var request = new CreateRecipeRequest(Model.Name);

        try
        {
            var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);

            /*
            TODO

            var createRecipeResult = await cookBookGrain.CreateRecipe(request);

            if (createRecipeResult.Succeeded)
            {
                snackbar.Add("Successfully created recipe.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(createRecipeResult.Value));
            }

            else
                snackbar.Add(createRecipeResult.Error.Message, Severity.Error);
                */
        }
        catch
        {
            snackbar.Add("An error occured while trying to create a recipe.", Severity.Error);
        }
    }

    private void Cancel() => MudDialog.Cancel();
}



public class CreateRecipeFormModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<StepFormModel> Steps { get; set; } = new List<StepFormModel>();
    public ICollection<IngredientFormModel> Ingredients { get; set; } = new List<IngredientFormModel>();
}

public class IngredientFormModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<StepFormModel> Steps { get; set; } = new List<StepFormModel>();
}

public class StepFormModel
{
    public string EquipmentType { get; set; } = string.Empty;
    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
}