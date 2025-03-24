using TheCodeKitchen.Application.Business.Services;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class AddKitchenCommandValidator : AbstractValidator<AddKitchenCommand>
{
    public AddKitchenCommandValidator()
    {
        RuleFor(kitchen => kitchen.Name)
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(kitchen => kitchen.GameId).NotEmpty();
    }
}

public sealed class AddKitchenCommandHandler(
    IGameRepository gameRepository,
    IKitchenService kitchenService,
    IDomainEventDispatcher domainEventDispatcher,
    IMapper mapper
) : IRequestHandler<AddKitchenCommand, Result<AddKitchenResponse>>
{
    public async Task<Result<AddKitchenResponse>> Handle(
        AddKitchenCommand request,
        CancellationToken cancellationToken = default
    ) => await
        TryAsync(() => gameRepository.GetGameWithKitchensById(request.GameId, cancellationToken))
            .Bind(game =>
                TryAsync(() => kitchenService.GenerateUniqueCode(cancellationToken: cancellationToken))
                    .Map(code => game.AddKitchen(request.Name, code))
                    .Map(kitchen => (game, kitchen))
            )
            .Bind(result =>
                TryAsync(() => gameRepository.UpdateAsync(result.game, cancellationToken))
                    .Map(_ => (result.game, result.kitchen))
            )
            .Do(result => domainEventDispatcher.DispatchEvents(result.game.GetEvents()))
            .Do(result => result.game.ClearEvents())
            .Map(result => mapper.Map<AddKitchenResponse>(result.kitchen))
            .Invoke();
}