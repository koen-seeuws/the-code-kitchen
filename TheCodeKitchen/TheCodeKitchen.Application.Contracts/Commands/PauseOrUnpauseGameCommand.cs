using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record PauseOrUnpauseGameCommand(Guid GameId) : IRequest<Result<PauseOrUnpauseGameResponse>>;