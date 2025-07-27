using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Response.Game;
using TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Pages;

public partial class Games(
    NavigationManager navigationManager,
    IDialogService dialogService,
    IClusterClient clusterClient
) : ComponentBase
{
    private ICollection<GetGameResponse>? GetGameResponses { get; set; }
    private string? ErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {
            var gameManagementGrain = clusterClient.GetGrain<IGameManagementGrain>(Guid.Empty);
            var result = await gameManagementGrain.GetGames();
            if (result.Succeeded)
                GetGameResponses = result.Value.ToList();
            else
                ErrorMessage = result.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the games.";
        }

        await base.OnInitializedAsync();
    }

    private void NavigateToGame(Guid gameId, bool started)
    {
        var link = $"/games/{gameId}";
        if(!started)
            link += "/pre-game-lobby";
        navigationManager.NavigateTo(link);
    }

    private async Task CreateGame()
    {
        var dialogOptions = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<CreateGameDialog>("Create Game", dialogOptions);
        var dialogResult = await dialog.Result;

        if (dialogResult is { Canceled: false, Data: CreateGameResponse createGameResponse })
            NavigateToGame(createGameResponse.Id, false);
    }
}