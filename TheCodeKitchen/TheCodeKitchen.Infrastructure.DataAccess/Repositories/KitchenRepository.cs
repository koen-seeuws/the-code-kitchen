using Microsoft.EntityFrameworkCore;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class KitchenRepository(TheCodeKitchenDbContext context)
    : Repository<Kitchen>(context), IKitchenRepository
{
    public async Task<bool> CodeExists(string code, CancellationToken cancellationToken = default)
        => await DbSet.AnyAsync(x => x.Code == code, cancellationToken);

    public async Task<ICollection<Kitchen>> GetByGameId(Guid gameId, CancellationToken cancellationToken = default)
        => await DbSet
            .AsNoTracking()
            .Where(x => x.GameId == gameId)
            .OrderBy(x => x.Created)
            .ToListAsync(cancellationToken);
}