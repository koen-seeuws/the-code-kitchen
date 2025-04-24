using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record CreateGameCommand(
    string? Name = null
) : IRequest<Result<CreateGameResponse>>;