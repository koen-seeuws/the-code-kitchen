using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Application.Contracts.Models;

const string apiUrl = "http://localhost:5169/";
const string kitchenCode = "";

var apiClient = new HttpClient { BaseAddress = new Uri(apiUrl) };

//Auth
var authRequest = new AuthenticationRequest("Koen", "Test123!", kitchenCode);
var httpResponse = await apiClient.PostAsJsonAsync("game/join", authRequest);
httpResponse.EnsureSuccessStatusCode();
var response = await httpResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
ArgumentNullException.ThrowIfNull(response);
ArgumentException.ThrowIfNullOrWhiteSpace(response.Token);

apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);

//SignalR
var connection = new HubConnectionBuilder()
    .WithUrl($"{apiUrl}KitchenHub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(response.Token)!;
    })
    .Build();

connection.On<NextMomentEvent>(nameof(NextMomentEvent), nextMomentEvent =>
{
    Console.WriteLine($"Kitchen: {nextMomentEvent.KitchenId} - Moment: {nextMomentEvent.Moment}");
});
