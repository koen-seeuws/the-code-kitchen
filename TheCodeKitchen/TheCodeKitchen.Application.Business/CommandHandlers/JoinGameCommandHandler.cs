using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Authentication;
using TheCodeKitchen.Application.Contracts.Interfaces.Authentication;
using TheCodeKitchen.Application.Contracts.Interfaces.Common;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class JoinGameCommandValidator : AbstractValidator<JoinGameCommand>
{
    public JoinGameCommandValidator()
    {
        RuleFor(x => x.KitchenCode)
            .NotEmpty();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}

public sealed class JoinGameCommandHandler(
    IGameRepository gameRepository,
    ICookRepository cookRepository,
    IDomainEventDispatcher domainEventDispatcher,
    IPasswordHashingService passwordHashingService,
    ISecurityTokenService securityTokenService
) : IRequestHandler<JoinGameCommand, Result<JoinGameResponse>>
{
    public async Task<Result<JoinGameResponse>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var cook = await cookRepository.FindCookByUsernameAndJoinCode(request.Username, request.KitchenCode, cancellationToken);
        if (cook != null)
        {
            if (!passwordHashingService.VerifyHashedPassword(cook.PasswordHash, request.Password))
                return new AuthenticationException("Invalid password");
            
        }
        else
        {
            var hashedPassword = passwordHashingService.HashPassword(request.Password);
            var game = await gameRepository.GetGameWithKitchensAndCooksByCode(request.KitchenCode, cancellationToken);
            cook = game.JoinGame(request.KitchenCode, request.Username, hashedPassword);
            await cookRepository.AddAsync(cook, cancellationToken);
            await domainEventDispatcher.DispatchAndClearEvents(game, cancellationToken);
        }
        
        var token = securityTokenService.GeneratePlayerToken(cook.Username, cook.KitchenId);
        var joinGameResponse = new JoinGameResponse(token);
        return joinGameResponse;
    }
}