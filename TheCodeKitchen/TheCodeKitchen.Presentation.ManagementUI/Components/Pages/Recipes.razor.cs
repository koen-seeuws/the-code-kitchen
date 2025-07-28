using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Response.CookBook;
using TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Pages;

public partial class Recipes(
    NavigationManager navigationManager,
    IDialogService dialogService,
    IClusterClient clusterClient
) : ComponentBase
{
    private ICollection<GetRecipeResponse>? GetRecipeResponses { get; set; }
    private string? ErrorMessage { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var cookBookGrain = clusterClient.GetGrain<ICookBookGrain>(Guid.Empty);
            var result = await cookBookGrain.GetRecipes();
            if (result.Succeeded)
                GetRecipeResponses = result.Value.ToList();
            else
                ErrorMessage = result.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the recipe.";
        }

        await base.OnInitializedAsync();
    }


    private async Task CreateRecipe()
    {
        var dialogParameters = new DialogParameters();
        var dialogOptions = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<CreateRecipeDialog>(
            "Create Recipe",
            dialogParameters,
            dialogOptions
        );
        var dialogResult = await dialog.Result;

        if (dialogResult is { Canceled: false, Data: CreateRecipeResponse createRecipeResponse })
            return;
    }
}