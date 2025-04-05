namespace TheCodeKitchen.Application.Contracts.Interfaces.Authentication;

public interface ISecurityTokenService
{
    public string GeneratePlayerToken(string username, Guid kitchenId);
}