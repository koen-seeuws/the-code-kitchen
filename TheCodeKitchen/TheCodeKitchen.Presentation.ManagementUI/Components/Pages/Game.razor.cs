using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Response.Game;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Pages;

public partial class Game(
    NavigationManager navigationManager,
    IDialogService dialogService,
    ISnackbar snackbar,
    IClusterClient clusterClient
) : ComponentBase
{
    [Parameter] public Guid GameId { get; set; }

    private GetGameResponse? GetGameResponse { get; set; }
    private ICollection<GetKitchenResponse>? GetKitchenResponses { get; set; }
    private bool? Paused { get; set; }
    public bool Advancing { get; set; }
    private string? ErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await LoadGame();
        await LoadKitchens();
        await base.OnInitializedAsync();
    }

    private async Task LoadGame()
    {
        try
        {
            GetGameResponse = null;
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var getGameResult = await gameGrain.GetGame();
            if (getGameResult.Succeeded)
            {
                GetGameResponse = getGameResult.Value;
                Paused = getGameResult.Value.Paused;
            }
            else
                ErrorMessage = getGameResult.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the game.";
        }
    }

    private async Task LoadKitchens()
    {
        try
        {
            GetKitchenResponses = null;
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var getKitchensResult = await gameGrain.GetKitchens();
            if (getKitchensResult.Succeeded)
                GetKitchenResponses = getKitchensResult.Value.ToList();
            else
                ErrorMessage = getKitchensResult.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the kitchens.";
        }
    }

    private async Task PauseOrUnpauseGame()
    {
        try
        {
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var pauseOrUnpauseResult = await gameGrain.PauseOrUnpauseGame();

            if (pauseOrUnpauseResult.Succeeded)
            {
                Paused = pauseOrUnpauseResult.Value.Paused;
                snackbar.Add($"Game has been {((bool)Paused ? "paused" : "unpaused")}.", Severity.Success);
            }
            else
            {
                snackbar.Add(pauseOrUnpauseResult.Error.Message, Severity.Error);
            }
        }
        catch
        {
            snackbar.Add("An error occurred while trying to pause or unpause the game.", Severity.Error);
        }
    }

    private async Task NextMoment()
    {
        try
        {
            Advancing = true;
            var gameGrain = clusterClient.GetGrain<IGameGrain>(GameId);
            var nextMomentResult = await gameGrain.NextMoment();

            if (!nextMomentResult.Succeeded)
            {
                snackbar.Add(nextMomentResult.Error.Message, Severity.Error);
            }
        }
        catch
        {
            snackbar.Add("An error occurred while trying to advance the game.", Severity.Error);
        }
        finally
        {
            Advancing = false;
        }
    }
}