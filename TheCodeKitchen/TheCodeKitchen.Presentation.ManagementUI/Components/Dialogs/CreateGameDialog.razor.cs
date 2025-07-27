using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests.Game;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

public partial class CreateGameDialog(ISnackbar snackbar, IClusterClient clusterClient) : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private MudForm Form { get; set; } = new();
    
    private CreateGameFormModel Model { get; set; } = new();

    private async Task Submit()
    {
        var request = new CreateGameRequest(Model.Name, Model.SpeedModifier, Model.Temperature);

        await Form.Validate();
        
        if (!Form.IsValid)
            return;

        try
        {
            var gameManagementGrain = clusterClient.GetGrain<IGameManagementGrain>(Guid.Empty);
            var createGameResult = await gameManagementGrain.CreateGame(request);

            if (createGameResult.Succeeded)
            {
                snackbar.Add("Successfully created game.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(createGameResult.Value));
            }
                
            else
                snackbar.Add(createGameResult.Error.Message, Severity.Error);
        }
        catch
        {
            snackbar.Add("An error occured while trying to create a game.", Severity.Error);
        }
    }

    private void Cancel() => MudDialog.Cancel();
}

public class CreateGameFormModel
{
    public string Name { get; set; } = string.Empty;
    public double SpeedModifier { get; set; } = 1.0;
    public double Temperature { get; set; } = 30.0;
}