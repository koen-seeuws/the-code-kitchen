

using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(game => game.Name)
            .MinimumLength(3)
            .MaximumLength(100);
    }
}

public sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IMapper mapper
) : IRequestHandler<CreateGameCommand, Result<CreateGameResponse>>
{
    public async Task<Result<CreateGameResponse>> Handle(
        CreateGameCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var count = await gameRepository.CountAllAsync(cancellationToken);
        var game = Game.Create(count, request.Name);
        await gameRepository.AddAsync(game, cancellationToken);
        await domainEventDispatcher.DispatchAndClearEvents(game, cancellationToken);
        var createGameResponse = mapper.Map<CreateGameResponse>(game);
        return createGameResponse;
    }
}