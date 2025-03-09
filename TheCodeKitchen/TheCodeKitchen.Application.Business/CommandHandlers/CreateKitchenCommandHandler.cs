using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using TheCodeKitchen.Application.Contracts.Commands;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Core.Domain.Exceptions;

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