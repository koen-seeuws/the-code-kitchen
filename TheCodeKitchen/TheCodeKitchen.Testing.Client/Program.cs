using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Models;

const string apiUrl = "http://localhost:5169/";
const string kitchenCode = "55JV";
const string username = "Koen";
const string password = "Test123!";

var apiClient = new HttpClient { BaseAddress = new Uri(apiUrl) };

//Auth
var authRequest = new AuthenticationRequest(username, password, kitchenCode);
var httpResponse = await apiClient.PostAsJsonAsync("game/join", authRequest);
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
    Console.WriteLine($"Kitchen: {nextMomentEvent.KitchenId} - Moment: {nextMomentEvent.Moment}");
});

await connection.StartAsync();

Console.ReadLine();
