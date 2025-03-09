namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(game => game.Name).MaximumLength(100);
    }
}

public sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IMapper mapper
) : IRequestHandler<CreateGameCommand, Result<CreateGameResponse>>
{
    public async Task<Result<CreateGameResponse>> Handle(CreateGameCommand request,
        CancellationToken cancellationToken = default)
    {
        
        return await gameRepository
            .CountAllAsync(cancellationToken)
            .Map(count => Game.Create(count, request.Name))
            .Bind(game => gameRepository.AddAsync(game, cancellationToken))
            .Map(mapper.Map<CreateGameResponse>)
            .Invoke();
    }
}