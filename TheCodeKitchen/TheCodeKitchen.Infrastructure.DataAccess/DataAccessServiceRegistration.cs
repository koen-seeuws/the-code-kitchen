using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCodeKitchen.Infrastructure.DataAccess.Repositories;

namespace TheCodeKitchen.Infrastructure.DataAccess;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration, string connectionStringKey = "Database")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"The ConnectionString {connectionStringKey} cannot be empty for DataAccess services");
        
        services.AddDbContext<TheCodeKitchenDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IKitchenRepository, KitchenRepository>();
        
        return services;
    }
}