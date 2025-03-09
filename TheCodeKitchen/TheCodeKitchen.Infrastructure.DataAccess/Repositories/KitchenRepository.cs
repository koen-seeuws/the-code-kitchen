using Microsoft.EntityFrameworkCore;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class KitchenRepository(TheCodeKitchenDbContext context)
    : Repository<Kitchen>(context), IKitchenRepository
{
    public TryAsync<Seq<string>> GetAllCodes(CancellationToken cancellationToken = default)
        => TryAsync(async () =>
        {
            var codes = await DbSet
                .Select(kitchen => kitchen.Code)
                .ToListAsync(cancellationToken);
            return codes.ToSeq();
        });
}