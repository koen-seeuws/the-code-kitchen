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
    IMapper mapper
) : IRequestHandler<CreateKitchenCommand, Result<CreateKitchenResponse>>
{
    public async Task<Result<CreateKitchenResponse>> Handle(CreateKitchenCommand request,
        CancellationToken cancellationToken = default)
        => await TryAsync(gameRepository.GetGameWithKitchensById(request.GameId, cancellationToken))
            .Bind<Game, Kitchen>(game =>
                TryAsync(kitchenRepository.GetAllCodes(cancellationToken))
                    .Map(codes => game.AddKitchen(codes, request.Name))
            )
            .Bind<Kitchen, Kitchen>(kitchen => TryAsync(kitchenRepository.AddAsync(kitchen, cancellationToken)))
            .Map(mapper.Map<CreateKitchenResponse>)
            .Invoke();
}