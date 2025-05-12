using System.Security.Claims;

namespace TheCodeKitchen.Infrastructure.Security.Extensions;

public static class TheCodeKitchenClaimsPrincipalExtensions
{
    public static Guid GetCookId(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetGuidClaim(TheCodeKitchenClaimTypes.CookId);
    
    public static Guid GetKitchenId(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetGuidClaim(TheCodeKitchenClaimTypes.KitchenId);
    
    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.GetClaim(TheCodeKitchenClaimTypes.Username);
}