using MediatR;
using TheCodeKitchen.Application.Contracts.Response;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record CreateKitchenCommand(
    long GameId,
    string? Name = null
) : IRequest<Result<CreateKitchenResponse>>;