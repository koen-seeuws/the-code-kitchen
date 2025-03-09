namespace TheCodeKitchen.Infrastructure.DataAccess.Repositories;

public class GameRepository(TheCodeKitchenDbContext context)
    : Repository<Game>(context), IGameRepository;