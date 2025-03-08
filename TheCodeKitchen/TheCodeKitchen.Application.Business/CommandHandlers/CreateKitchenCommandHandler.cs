using LanguageExt.Common;
using MediatR;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Application.Contracts.Response;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public class CreateKitchenCommandHandler(
    IGameRepository gameRepository,
    IKitchenRepository kitchenRepository
) : IRequestHandler<CreateKitchenCommand, Result<CreateKitchenResponse>>
{
    public async Task<Result<CreateKitchenResponse>> Handle(CreateKitchenCommand request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetByIdAsync(request.GameId, cancellationToken);
        var codes = await kitchenRepository.GetAllCodes(cancellationToken);
    }
}