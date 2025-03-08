using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Infrastructure.DataAccess.Abstractions;
using TheCodeKitchen.Infrastructure.DataAccess.Entities;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class KitchenRepository(DbContext context, IMapper mapper) : Repository<Kitchen, KitchenModel>(context, mapper), IKitchenRepository
{
    public async Task<IEnumerable<string>> GetAllCodes(CancellationToken cancellationToken)
    {
        return await DbSet.Select(kitchen => kitchen.Code).ToListAsync(cancellationToken: cancellationToken);
    }
}