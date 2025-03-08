using LanguageExt.Common;
using MediatR;
using TheCodeKitchen.Application.Contracts.Response;


namespace TheCodeKitchen.Application.Contracts.Commands;

public record CreateGameCommand(
    string? Name = null
) : IRequest<Result<CreateGameResponse>>;