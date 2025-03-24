using MediatR;
using TheCodeKitchen.Application.Contracts.Response;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record AddKitchenCommand(
    Guid GameId,
    string? Name = null
) : IRequest<Result<AddKitchenResponse>>;