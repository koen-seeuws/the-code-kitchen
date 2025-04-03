namespace TheCodeKitchen.Application.Contracts.Interfaces.Authentication;

public interface IAuthenticationService
{
    public string HashPassword(string password);
    public string GeneratePlayerToken(Guid kitchenId, string username);
}