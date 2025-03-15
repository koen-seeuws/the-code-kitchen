namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class CreateKitchenCommandValidator : AbstractValidator<CreateKitchenCommand>
{
    public CreateKitchenCommandValidator()
    {
        RuleFor(kitchen => kitchen.Name)
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(kitchen => kitchen.GameId).NotEmpty();
    }
}

public sealed class CreateKitchenCommandHandler(
    IGameRepository gameRepository,
    IKitchenRepository kitchenRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IMapper mapper
) : IRequestHandler<CreateKitchenCommand, Result<CreateKitchenResponse>>
{
    public async Task<Result<CreateKitchenResponse>> Handle(
        CreateKitchenCommand request,
        CancellationToken cancellationToken = default
    ) => await
        TryAsync(() => gameRepository.GetGameWithKitchensById(request.GameId, cancellationToken))
            .Bind(game =>
                TryAsync(() => kitchenRepository.GetAllCodes(cancellationToken))
                    .Map(codes => game.AddKitchen(codes, request.Name))
                    .Map(kitchen => (game, kitchen))
            )
            .Bind(result =>
                TryAsync(() => gameRepository.UpdateAsync(result.game, cancellationToken))
                    .Map(_ => (result.game, result.kitchen))
            )
            .Do(result => domainEventDispatcher.DispatchEvents(result.game.GetEvents()))
            .Do(result => result.game.ClearEvents())
            .Map(result => mapper.Map<CreateKitchenResponse>(result.kitchen))
            .Invoke();
}