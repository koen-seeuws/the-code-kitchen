using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Presentation.API.Cook.Models;

const string apiUrl = "https://ca-tck-cook-api.proudbeach-fbb36fdd.westeurope.azurecontainerapps.io/";
const string kitchenCode = "MJYJ";
const string username = "Koen 5";
const string password = "Test123!";

var apiClient = new HttpClient { BaseAddress = new Uri(apiUrl) };

//Auth
var authRequest = new AuthenticationRequest(username, password);
var httpResponse = await apiClient.PostAsJsonAsync($"kitchen/{kitchenCode}/join", authRequest);
httpResponse.EnsureSuccessStatusCode();
var response = await httpResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
ArgumentNullException.ThrowIfNull(response);
ArgumentException.ThrowIfNullOrWhiteSpace(response.Token);

apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

//SignalR
var connection = new HubConnectionBuilder()
    .WithUrl($"{apiUrl}kitchenhub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(response.Token)!;
    })
    .WithAutomaticReconnect()
    .Build();

connection.On<NextMomentEvent>(nameof(NextMomentEvent), nextMomentEvent =>
{
    Console.WriteLine($"Kitchen: {nextMomentEvent.GameId} - Moment: {nextMomentEvent.Moment}");
});

await connection.StartAsync();

Console.ReadLine();
