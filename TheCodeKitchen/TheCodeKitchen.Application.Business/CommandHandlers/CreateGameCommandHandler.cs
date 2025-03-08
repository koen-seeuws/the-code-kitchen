using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Core.Domain;

namespace TheCodeKitchen.Application.Business.CommandHandlers;

public sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
    }
}

public sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IMapper mapper
) : IRequestHandler<CreateGameCommand, Result<CreateGameResponse>>
{
    public async Task<Result<CreateGameResponse>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {


        var newGame = Game.Create()

        // 3️⃣ Try saving the game
        var result = await gameRepository.AddAsync(newGame)
            .Map(savedGame => new CreateGameResponse(savedGame.Id, savedGame.Name));

        // 4️⃣ Return the result (success or failure)
        return result;
    }
}