using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Application.Contracts.Exception;
using TheCodeKitchen.Core.Domain.Exceptions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class GameRepository(TheCodeKitchenDbContext context)
    : Repository<Game>(context), IGameRepository
{
    public async Task<Game> GetGameWithKitchensById(Guid gameId, CancellationToken cancellationToken = default)
    {
        var game = await DbSet
            .Include(game => game.Kitchens)
            .FirstOrDefaultAsync(game => game.Id == gameId, cancellationToken);
        if (game == null)
            throw new NotFoundException($"Game with kitchens was not found using id {gameId}");
        return game;
    }

    public async Task<Game> GetGameWithKitchensAndCooksByCode(string joinCode, CancellationToken cancellationToken = default)
    {
        var game = await DbSet
            .Include(game => game.Kitchens)
            .ThenInclude(kitchen => kitchen.Cooks)
            .FirstOrDefaultAsync(game => game.Kitchens.Any(kitchen => kitchen.Code == joinCode), cancellationToken);
        if (game == null)
            throw new NotFoundException($"Game with kitchens and cook was not found using code {joinCode}");
        return game;
    }
}