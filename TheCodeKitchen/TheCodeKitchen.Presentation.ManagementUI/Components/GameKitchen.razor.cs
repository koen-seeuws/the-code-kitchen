using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using TheCodeKitchen.Application.Contracts.Events.Kitchen;
using TheCodeKitchen.Application.Contracts.Events.KitchenOrder;
using TheCodeKitchen.Application.Contracts.Response.Kitchen;

namespace TheCodeKitchen.Presentation.ManagementUI.Components;

public partial class GameKitchen(
    NavigationManager navigationManager,
    ISnackbar snackbar,
    IClusterClient clusterClient,
    IMapper mapper
) : ComponentBase, IAsyncDisposable
{
    [Parameter] public GetKitchenResponse Kitchen { get; set; }

    private HubConnection? _kitchenHubConnection;
    private HubConnection? _kitchenOrderHubConnection;


    protected override async Task OnInitializedAsync()
    {
        //TODO: Load initial data
        await ListenToKitchenEvents();
        await ListenToKitchenOrderEvents();
    }

    private async Task ListenToKitchenEvents()
    {
        if (_kitchenHubConnection is not null)
            await _kitchenHubConnection.DisposeAsync();

        _kitchenHubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri($"/KitchenHub?kitchenId={Kitchen.Id}"))
            .Build();
        
        _kitchenHubConnection.On(nameof(KitchenRatingUpdatedEvent), async (KitchenRatingUpdatedEvent @event) =>
        {
            //TODO
            await InvokeAsync(StateHasChanged);
        });

        _kitchenHubConnection.On(nameof(MessageDeliveredEvent), async (MessageDeliveredEvent @event) =>
        {
            //TODO
            await InvokeAsync(StateHasChanged);
        });

        try
        {
            await _kitchenHubConnection.StartAsync();
        }
        catch
        {
            snackbar.Add("Failed to start listening to kitchen events", Severity.Error);
        }
    }

    private async Task ListenToKitchenOrderEvents()
    {
        if (_kitchenOrderHubConnection is not null)
            await _kitchenOrderHubConnection.DisposeAsync();

        _kitchenOrderHubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri($"/KitchenOrderHub?kitchenId={Kitchen.Id}"))
            .Build();

        _kitchenOrderHubConnection.On(nameof(NewKitchenOrderEvent), async (NewKitchenOrderEvent @event) =>
        {
            //TODO
            await InvokeAsync(StateHasChanged);
        });
        
        _kitchenOrderHubConnection.On(nameof(KitchenOrderFoodDeliveredEvent), async (KitchenOrderFoodDeliveredEvent @event) =>
        {
            //TODO
            await InvokeAsync(StateHasChanged);
        });
        
        _kitchenOrderHubConnection.On(nameof(KitchenOrderCompletedEvent), async (KitchenOrderCompletedEvent @event) =>
        {
            //TODO
            await InvokeAsync(StateHasChanged);
        });

        try
        {
            await _kitchenOrderHubConnection.StartAsync();
        }
        catch
        {
            snackbar.Add("Failed to start listening to kitchen events", Severity.Error);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_kitchenHubConnection != null) await _kitchenHubConnection.DisposeAsync();
        if (_kitchenOrderHubConnection != null) await _kitchenOrderHubConnection.DisposeAsync();
    }
}