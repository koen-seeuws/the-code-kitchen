

using TheCodeKitchen.Application.Contracts.Interfaces.Common;

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
    ) => await
        TryAsync(gameRepository.CountAllAsync(cancellationToken))
            .Map(count => Game.Create(count, request.Name))
            .Bind(game => TryAsync(gameRepository.AddAsync(game, cancellationToken)))
            .Do(game => domainEventDispatcher.DispatchEvents(game.GetEvents()))
            .Do(game => game.ClearEvents())
            .Map(mapper.Map<CreateGameResponse>)
            .Invoke();
}