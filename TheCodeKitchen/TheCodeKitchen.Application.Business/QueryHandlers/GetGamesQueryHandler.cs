using System.Collections.Immutable;
using TheCodeKitchen.Application.Contracts.Queries;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.QueryHandlers;

public class GetGamesQueryHandler(
    IGameRepository gameRepository,
    IMapper mapper
) : IRequestHandler<GetGamesQuery, Result<IEnumerable<GetGamesResponse>>>
{
    public async Task<Result<IEnumerable<GetGamesResponse>>> Handle(GetGamesQuery request,
        CancellationToken cancellationToken)
    {
        var games = await gameRepository.GetAllAsync(cancellationToken);
        var gameResponses = mapper.Map<List<GetGamesResponse>>(games);
        return gameResponses;
    }
}