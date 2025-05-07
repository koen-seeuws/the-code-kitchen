namespace TheCodeKitchen.Application.Contracts.Models;

public record AuthenticationRequest(string Username, string Password, string KitchenCode);