using TheCodeKitchen.Core.Domain.Events;

namespace TheCodeKitchen.Core.Domain;

public partial class Game
{
    private const short CodeGenerationAttemptsPerLength = 10;
    private static readonly char[] ValidCodeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
    private const short MinimumCodeLength = 4;
    
    public Kitchen AddKitchen(ICollection<string> existingCodes, string? name = null)
    {
        // Make sure the kitchen has a name
        name = name?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            name = $"Kitchen {Kitchens.Count + 1}";
        
        // Generate a unique code for the new kitchen
        string code;
        var attempts = 0;
        var length = MinimumCodeLength;

        do
        {
            var codeChars = new char[length];
            for (var i = 0; i < length; i++)
            {
                var randomValidCharacterIndex = Random.Shared.Next(ValidCodeCharacters.Length);
                codeChars[i] = ValidCodeCharacters[randomValidCharacterIndex];
            }
            code = new string(codeChars);

            attempts++;

            if (attempts < CodeGenerationAttemptsPerLength) continue;

            attempts = 0;
            length++;
        } while (existingCodes.Contains(code));

        // Create a new Kitchen instance
        var kitchen = new Kitchen(name, code, Id);
        Kitchens.Add(kitchen);
        
        RaiseEvent(new KitchenCreatedEvent(kitchen.Id, kitchen.Name, kitchen.Code));
        
        return kitchen;
    }
}