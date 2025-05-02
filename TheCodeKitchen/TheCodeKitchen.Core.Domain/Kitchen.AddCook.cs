
namespace TheCodeKitchen.Core.Domain;

public partial class Kitchen
{
    public Cook AddCook(string username, string passwordHash)
    {
        username = username.Trim();

        /*
        if (string.IsNullOrWhiteSpace(username))
            throw new NotEmptyException("Your username cannot be empty");
        if (Cooks.Any(cook => string.Equals(username, username, StringComparison.CurrentCultureIgnoreCase)))
            throw new NotUniqueException($"The username ${username} already exists in the kitchen with code {Code}");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new NotEmptyException("Password cannot be empty");
            */
        
        var cook = new Cook(Guid.Empty, username, passwordHash, Id);
        Cooks.Add(cook.Id);
        return cook;
    }
}