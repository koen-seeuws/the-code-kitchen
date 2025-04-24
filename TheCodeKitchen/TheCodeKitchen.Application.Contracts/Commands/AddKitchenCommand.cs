using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record AddKitchenCommand(
    Guid GameId,
    string? Name = null
) : IRequest<Result<AddKitchenResponse>>;