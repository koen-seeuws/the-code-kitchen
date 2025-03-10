using Microsoft.EntityFrameworkCore;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class KitchenRepository(TheCodeKitchenDbContext context)
    : Repository<Kitchen>(context), IKitchenRepository
{
    public async Task<ICollection<string>> GetAllCodes(CancellationToken cancellationToken = default)
        => await DbSet
            .Select(kitchen => kitchen.Code)
            .ToListAsync(cancellationToken);
}