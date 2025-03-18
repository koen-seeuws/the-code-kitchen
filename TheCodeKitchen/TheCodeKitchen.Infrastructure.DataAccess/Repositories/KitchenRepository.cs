using Microsoft.EntityFrameworkCore;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class KitchenRepository(TheCodeKitchenDbContext context)
    : Repository<Kitchen>(context), IKitchenRepository
{
    public async Task<bool> CodeExists(string code, CancellationToken cancellationToken = default)
        => await DbSet.AnyAsync(x => x.Code == code, cancellationToken);
}