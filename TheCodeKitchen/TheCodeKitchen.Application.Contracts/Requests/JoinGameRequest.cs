namespace TheCodeKitchen.Application.Contracts.Requests;

public record JoinGameRequest(
    string Username,
    string Password,
    string KitchenCode
);