namespace TheCodeKitchen.Application.Contracts.Interfaces.Security;

public interface IPasswordHashingService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}