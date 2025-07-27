using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Kitchen;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

public partial class CreateKitchenDialog(ISnackbar snackbar, IClusterClient clusterClient) : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    
    [Parameter] public Guid GameId { get; set; }

    private MudForm Form { get; set; } = new();
    
    private CreateKitchenFormModel Model { get; set; } = new();

    private async Task Submit()
    {
        var request = new CreateKitchenRequest(Model.Name, GameId);

        await Form.Validate();
        
        if (!Form.IsValid)
            return;

        try
        {
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var createKitchenResult = await gameGrain.CreateKitchen(request);

            if (createKitchenResult.Succeeded)
            {
                snackbar.Add("Successfully created kitchen.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(createKitchenResult.Value));
            }
                
            else
                snackbar.Add(createKitchenResult.Error.Message, Severity.Error);
        }
        catch
        {
            snackbar.Add("An error occured while trying to create a kitchen.", Severity.Error);
        }
    }

    private void Cancel() => MudDialog.Cancel();
}

public class CreateKitchenFormModel
{
    public string Name { get; set; } = string.Empty;
}