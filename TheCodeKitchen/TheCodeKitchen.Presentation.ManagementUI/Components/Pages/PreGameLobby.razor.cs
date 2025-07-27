using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Response.Game;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;
using TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Pages;

public partial class PreGameLobby(
    NavigationManager navigationManager,
    IDialogService dialogService,
    ISnackbar snackbar,
    IClusterClient clusterClient
) : ComponentBase
{
    [Parameter] public Guid GameId { get; set; }

    private GetGameResponse? GetGameResponse { get; set; }
    private ICollection<GetKitchenResponse>? GetKitchenResponses { get; set; }

    private string? ErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);

            var getGameResult = await gameGrain.GetGame();
            if (getGameResult.Succeeded)
                GetGameResponse = getGameResult.Value;
            else
                ErrorMessage = getGameResult.Error.Message;

            var getKitchensResult = await gameGrain.GetKitchens();
            if (getKitchensResult.Succeeded)
                GetKitchenResponses = getKitchensResult.Value.ToList();
            else
                ErrorMessage = getKitchensResult.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the necessary game information";
        }

        await base.OnInitializedAsync();
    }

    private async Task CreateKitchen()
    {
        var dialogParameters = new DialogParameters { { nameof(CreateKitchenDialog.GameId), GameId } };
        var dialogOptions = new DialogOptions { CloseOnEscapeKey = true };

        var dialog =
            await dialogService.ShowAsync<CreateKitchenDialog>("Create Kitchen", dialogParameters, dialogOptions);
        var dialogResult = await dialog.Result;

        if (dialogResult is { Canceled: false, Data: CreateKitchenResponse createKitchenResponse })
        {
            var kitchen = new GetKitchenResponse(
                createKitchenResponse.Id,
                createKitchenResponse.Name,
                createKitchenResponse.Code,
                createKitchenResponse.Rating,
                createKitchenResponse.Game
            );
            GetKitchenResponses?.Add(kitchen);
        }
    }

    private async Task StartGame()
    {
        try
        {
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var startGameResult = await gameGrain.StartGame();
            if (startGameResult.Succeeded)
            {
                snackbar.Add("Game started successfully.", Severity.Success);
                navigationManager.NavigateTo($"/games/{GameId}");
            }
            else
            {
                snackbar.Add(startGameResult.Error.Message, Severity.Error);
            }
        }
        catch
        {
            snackbar.Add("An error occurred while starting the game.", Severity.Error);
        }
    }
}