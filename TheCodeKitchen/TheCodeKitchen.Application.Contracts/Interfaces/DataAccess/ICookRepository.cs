namespace TheCodeKitchen.Application.Contracts.Interfaces.DataAccess;

public interface ICookRepository : IRepository<Cook>
{
    Task<Cook?> FindCookByUsernameAndJoinCode(
        string username,
        string joinCode,
        CancellationToken cancellationToken = default
    );
}