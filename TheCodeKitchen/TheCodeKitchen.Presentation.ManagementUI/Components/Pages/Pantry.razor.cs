using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Response.Game;
using TheCodeKitchen.Application.Contracts.Response.Pantry;
using TheCodeKitchen.Presentation.ManagementUI.Components.Dialogs;

namespace TheCodeKitchen.Presentation.ManagementUI.Components.Pages;

public partial class Pantry(
    NavigationManager navigationManager,
    IDialogService dialogService,
    IClusterClient clusterClient
) : ComponentBase
{
    private ICollection<GetIngredientResponse>? GetIngredientResponses { get; set; }
    private string? ErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {
            var pantryGrain = clusterClient.GetGrain<IPantryGrain>(Guid.Empty);
            var result = await pantryGrain.GetIngredients();
            if (result.Succeeded)
                GetIngredientResponses = result.Value.ToList();
            else
                ErrorMessage = result.Error.Message;
        }
        catch
        {
            ErrorMessage = "An error occurred while retrieving the ingredients.";
        }

        await base.OnInitializedAsync();
    }

    private async Task CreateIngredient()
    { ;
        var dialog = await dialogService.ShowAsync<CreateIngredientDialog>("Create Ingredient");
        var dialogResult = await dialog.Result;

        if (dialogResult is { Canceled: false, Data: CreateIngredientResponse createIngredientResponse })
        {
            var ingredient = new GetIngredientResponse(createIngredientResponse.Name);
            GetIngredientResponses?.Add(ingredient);
        }
    }
}