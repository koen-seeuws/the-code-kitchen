using Microsoft.EntityFrameworkCore;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class CookRepository(TheCodeKitchenDbContext context) : Repository<Cook>(context), ICookRepository
{
    public async Task<Cook?> FindCookByUsernameAndJoinCode(string username, string joinCode,
        CancellationToken cancellationToken = default)
    {
        username = username.ToLower().Trim();
        return await DbSet
            .Where(cook => cook.Username.ToLower().Trim() == username && cook.Kitchen.Code == joinCode)
            .FirstOrDefaultAsync(cancellationToken);
    }
}