using MediatR;
using TheCodeKitchen.Core.Shared.Monads;

namespace TheCodeKitchen.Application.Contracts.Commands;

public class CreateGameCommand : IRequest<Result<CreateGameRequestResult>>;

public class CreateGameRequestResult
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Paused { get; set; }
}