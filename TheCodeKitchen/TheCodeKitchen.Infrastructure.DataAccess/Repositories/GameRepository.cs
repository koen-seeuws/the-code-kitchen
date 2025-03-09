using TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;
using TheCodeKitchen.Core.Domain;
using TheCodeKitchen.Infrastructure.DataAccess.Abstractions;

namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class GameRepository(TheCodeKitchenDbContext context)
    : Repository<Game>(context), IGameRepository
{
}