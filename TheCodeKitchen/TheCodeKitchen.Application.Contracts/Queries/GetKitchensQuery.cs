using MediatR;
using TheCodeKitchen.Application.Contracts.Response;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Contracts.Queries;

public record GetKitchensQuery(Guid GameId) : IRequest<Result<IEnumerable<GetKitchenResponse>>>;