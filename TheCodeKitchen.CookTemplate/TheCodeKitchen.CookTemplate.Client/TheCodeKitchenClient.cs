using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.CookTemplate.Contracts.Events.Communication;
using TheCodeKitchen.CookTemplate.Contracts.Events.Order;
using TheCodeKitchen.CookTemplate.Contracts.Events.Timer;
using TheCodeKitchen.CookTemplate.Contracts.Reponses.Authentication;
using TheCodeKitchen.CookTemplate.Contracts.Requests.Authentication;

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

    private async Task Authenticate(CancellationToken cancellationToken = default)
    {
        var authenticationRequest = new AuthenticationRequestDto(_username, _password);
        var httpResponse = await _httpClient.PostAsJsonAsync($"kitchen/{_kitchenCode}/join", authenticationRequest, cancellationToken: cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var authenticationResponse = await httpResponse.Content.ReadFromJsonAsync<AuthenticationResponseDto>(cancellationToken: cancellationToken);

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