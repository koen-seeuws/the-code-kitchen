using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.CookTemplate.Contracts.Events.Communication;
using TheCodeKitchen.CookTemplate.Contracts.Events.Order;
using TheCodeKitchen.CookTemplate.Contracts.Events.Timer;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Authentication;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Communication;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.CookBook;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Food;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Orders;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Pantry;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Timer;
using TheCodeKitchen.CookTemplate.Contracts.Requests.Authentication;
using TheCodeKitchen.CookTemplate.Contracts.Requests.Communication;
using TheCodeKitchen.CookTemplate.Contracts.Requests.Timer;

namespace TheCodeKitchen.CookTemplate.Client;

public class TheCodeKitchenClient
{
    private readonly string _username;
    private readonly string _password;
    private readonly string _kitchenCode;

    private readonly Uri _baseUrl;
    private readonly HttpClient _httpClient;
    private string? _token;
    private HubConnection? _cookHubConnection;

    public TheCodeKitchenClient(string username, string password, string kitchenCode, string baseUrl)
    {
        _username = username;
        _password = password;
        _kitchenCode = kitchenCode;

        if (!baseUrl.EndsWith('/'))
            baseUrl += '/';

        _baseUrl = new Uri(baseUrl);
        _httpClient = new HttpClient { BaseAddress = _baseUrl };
    }

    public async Task Connect(
        Action<KitchenOrderCreatedEvent> onKitchenOrderCreated,
        Action<TimerElapsedEvent> onTimerElapsed,
        Action<MessageReceivedEvent> onMessageReceived,
        CancellationToken cancellationToken = default
    )
    {
        await Authenticate(cancellationToken);
        await ListenToCookHubEvents(onKitchenOrderCreated, onTimerElapsed, onMessageReceived, cancellationToken);
    }

    // Communication
    public async Task SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsJsonAsync("Communication/SendMessage", request,
            cancellationToken: cancellationToken);
    
    public async Task<ReadMessageResponse[]> ReadMessages(CancellationToken cancellationToken = default)
        => await _httpClient.GetFromJsonAsync<ReadMessageResponse[]>("Communication/ReadMessages",
            cancellationToken: cancellationToken) ?? [];

    public async Task ConfirmMessage(ConfirmMessageRequest request, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsJsonAsync("Communication/ConfirmMessage", request,
            cancellationToken: cancellationToken);

    // CookBook
    public async Task<GetRecipeResponse[]> ReadRecipes(CancellationToken cancellationToken = default)
        => await _httpClient.GetFromJsonAsync<GetRecipeResponse[]>("CookBook/Read",
            cancellationToken: cancellationToken) ?? [];

    // Equipment
    public async Task AddFoodToEquipment(string equipment, int number, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsync($"Equipment/{equipment}/{number}/AddFood", null, cancellationToken);

    public async Task<TakeFoodResponse?> TakeFoodFromEquipment(string equipment, int number,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync($"Equipment/{equipment}/{number}/TakeFood", null, cancellationToken);
        return await response.Content.ReadFromJsonAsync<TakeFoodResponse>(cancellationToken: cancellationToken) ?? null;
    }

    // Orders
    public async Task<GetOpenOrderResponse[]> ViewOpenOrders(CancellationToken cancellationToken = default)
        => await _httpClient.GetFromJsonAsync<GetOpenOrderResponse[]>("Orders/ViewOpen",
            cancellationToken: cancellationToken) ?? [];

    public async Task DeliverFoodToOrder(long orderNumber, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsync($"Orders/{orderNumber}/Deliver", null, cancellationToken);

    public async Task CompleteOrder(long orderNumber, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsync($"Orders/{orderNumber}/Complete", null, cancellationToken);

    // Pantry
    public async Task<GetIngredientResponse[]> PantryInventory(CancellationToken cancellationToken = default)
        => await _httpClient.GetFromJsonAsync<GetIngredientResponse[]>("Pantry/Inventory",
            cancellationToken: cancellationToken) ?? [];

    public async Task<TakeFoodResponse?> TakeFoodFromPantry(string ingredient,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync($"Pantry/{ingredient}/TakeFood", null, cancellationToken);
        return await response.Content.ReadFromJsonAsync<TakeFoodResponse>(cancellationToken: cancellationToken) ?? null;
    }

    // Timer
    public async Task SetTimer(SetTimerRequest request, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsJsonAsync("Timer/Set", request, cancellationToken: cancellationToken);

    public async Task<GetTimerResponse[]> GetTimers(CancellationToken cancellationToken = default)
        => await _httpClient.GetFromJsonAsync<GetTimerResponse[]>("Timer/Get", cancellationToken) ?? [];

    public async Task StopTimer(StopTimerRequest request, CancellationToken cancellationToken = default)
        => await _httpClient.PostAsJsonAsync("Timer/Stop", request, cancellationToken: cancellationToken);

    // Kitchen (/Authentication)
    private async Task Authenticate(CancellationToken cancellationToken = default)
    {
        var authenticationRequest = new AuthenticationRequestDto(_username, _password);
        var httpResponse = await _httpClient.PostAsJsonAsync($"kitchen/{_kitchenCode}/join", authenticationRequest,
            cancellationToken: cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var authenticationResponse =
            await httpResponse.Content.ReadFromJsonAsync<AuthenticationResponseDto>(
                cancellationToken: cancellationToken);

        ArgumentNullException.ThrowIfNull(authenticationResponse);
        ArgumentException.ThrowIfNullOrWhiteSpace(authenticationResponse.Token);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token = authenticationResponse.Token);
    }

    private async Task ListenToCookHubEvents(
        Action<KitchenOrderCreatedEvent> onKitchenOrderCreated,
        Action<TimerElapsedEvent> onTimerElapsed,
        Action<MessageReceivedEvent> onMessageReceived,
        CancellationToken cancellationToken = default
    )
    {
        _cookHubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri(_baseUrl, "CookHub"),
                options => { options.AccessTokenProvider = () => Task.FromResult(_token); })
            .WithAutomaticReconnect()
            .Build();

        _cookHubConnection.On(nameof(KitchenOrderCreatedEvent), onKitchenOrderCreated);
        _cookHubConnection.On(nameof(TimerElapsedEvent), onTimerElapsed);
        _cookHubConnection.On(nameof(MessageReceivedEvent), onMessageReceived);

        await _cookHubConnection.StartAsync(cancellationToken);
    }
}