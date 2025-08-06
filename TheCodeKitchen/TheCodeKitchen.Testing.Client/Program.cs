using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using TheCodeKitchen.Application.Contracts.Events;
using TheCodeKitchen.Presentation.API.Cook.Models;

const string apiUrl = "http://localhost:5169/";
const string kitchenCode = "BYE5";
const string username = "KOEN2";
const string password = "TEST";

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
var kitchenConnection = new HubConnectionBuilder()
    .WithUrl($"{apiUrl}kitchenhub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(response.Token)!;
    })
    .WithAutomaticReconnect()
    .Build();
var cookConnection = new HubConnectionBuilder()
    .WithUrl($"{apiUrl}cookhub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(response.Token)!;
    })
    .WithAutomaticReconnect()
    .Build();

kitchenConnection.On<NewKitchenOrderEvent>(nameof(NewKitchenOrderEvent), kitchenOrderEvent =>
{
    Console.WriteLine($"KitchenOrder: {kitchenOrderEvent.Number} ");
});

cookConnection.On<TimerElapsedEvent>(nameof(TimerElapsedEvent), timerElapsedEvent =>
{
    Console.WriteLine($"Timer elapsed: {timerElapsedEvent.Number} ");
});

cookConnection.On<MessageReceivedEvent>(nameof(MessageReceivedEvent), messageReceivedEvent =>
{
    Console.WriteLine($"Message received: {messageReceivedEvent.Number} ");
});

await kitchenConnection.StartAsync();
await cookConnection.StartAsync();

Console.ReadLine();
