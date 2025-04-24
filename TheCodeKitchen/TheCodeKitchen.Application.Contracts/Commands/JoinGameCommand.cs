using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record JoinGameCommand(
    string Username,
    string Password,
    string KitchenCode
) : IRequest<Result<JoinGameResponse>>;