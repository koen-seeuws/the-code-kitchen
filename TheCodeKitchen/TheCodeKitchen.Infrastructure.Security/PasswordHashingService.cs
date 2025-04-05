using Microsoft.AspNetCore.Identity;
using TheCodeKitchen.Application.Contracts.Interfaces.Authentication;

namespace TheCodeKitchen.Infrastructure.Security;

public class PasswordHashingService : IPasswordHashingService
{
    private readonly PasswordHasher<string> _passwordHasher = new();

    public string HashPassword(string password)
        => _passwordHasher.HashPassword(string.Empty, password);
    
    public bool VerifyHashedPassword(string hashedPassword, string password)
        => _passwordHasher.VerifyHashedPassword(string.Empty, hashedPassword, password) ==
           PasswordVerificationResult.Success;
}