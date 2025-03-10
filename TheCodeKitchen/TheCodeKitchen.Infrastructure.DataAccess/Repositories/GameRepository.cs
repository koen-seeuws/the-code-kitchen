using Microsoft.EntityFrameworkCore;
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
}