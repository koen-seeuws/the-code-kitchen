using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.CookBook;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

public partial class CreateRecipeDialog(ISnackbar snackbar, IClusterClient clusterClient) : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private MudForm Form { get; set; } = new();
    
    private CreateRecipeFormModel Model { get; set; } = new();

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
}