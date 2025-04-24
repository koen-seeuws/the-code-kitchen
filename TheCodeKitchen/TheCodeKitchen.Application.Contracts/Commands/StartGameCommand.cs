using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;
using TheCodeKitchen.Core.Shared;

namespace TheCodeKitchen.Application.Contracts.Commands;

public record StartGameCommand(Guid GameId) : IRequest<Result<TheCodeKitchenUnit>>;