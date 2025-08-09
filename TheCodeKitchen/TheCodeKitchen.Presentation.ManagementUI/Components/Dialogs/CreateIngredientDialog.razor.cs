using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Pantry;
using TheCodeKitchen.Presentation.ManagementUI.Models;
using TheCodeKitchen.Presentation.ManagementUI.Models.FormModels;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

public partial class CreateIngredientDialog(ISnackbar snackbar, IClusterClient clusterClient) : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private MudForm Form { get; set; } = new();
    
    private CreateIngredientFormModel Model { get; set; } = new();

    private async Task Submit()
    {
        await Form.Validate();
        if (!Form.IsValid)
            return;
        
        try
        {
            var request = new CreateIngredientRequest(Model.Name);
            var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
            var createIngredientResult = await pantryGrain.CreateIngredient(request);

            if (createIngredientResult.Succeeded)
            {
                snackbar.Add("Successfully created ingredient.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(createIngredientResult.Value));
            }
                
            else
                snackbar.Add(createIngredientResult.Error.Message, Severity.Error);
        }
        catch
        {
            snackbar.Add("An error occured while trying to create an ingredient.", Severity.Error);
        }
    }

    private void Cancel() => MudDialog.Cancel();
}