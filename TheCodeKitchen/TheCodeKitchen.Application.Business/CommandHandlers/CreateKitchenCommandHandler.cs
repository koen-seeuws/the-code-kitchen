namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class CreateKitchenCommandValidator : AbstractValidator<CreateKitchenCommand>
{
    public CreateKitchenCommandValidator()
    {
        RuleFor(kitchen => kitchen.Name).MaximumLength(100);
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
    {
        return await gameRepository
            .GetByIdAsync(request.GameId, cancellationToken)
            .Bind(game => kitchenRepository
                .GetAllCodes(cancellationToken)
                .Map(codes => game.AddKitchen(codes.ToList(), request.Name))
                .Bind(kitchen => kitchenRepository.AddAsync(kitchen, cancellationToken))
                .Map(mapper.Map<CreateKitchenResponse>)
            )
            .Invoke();
    }
}