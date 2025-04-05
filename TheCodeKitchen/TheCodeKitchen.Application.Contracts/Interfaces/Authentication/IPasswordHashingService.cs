namespace TheCodeKitchen.Application.Contracts.Interfaces.Authentication;

public interface IPasswordHashingService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}