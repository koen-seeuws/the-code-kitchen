using TheCodeKitchen.Application.Contracts.Interfaces.Common;

namespace TheCodeKitchen.Infrastructure.Common.Services;

public class CodeGenerator : ICodeGenerator
{
    public string GenerateCode(int length = 4, string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        var validCharacters = characters.ToCharArray();
        var chars = new char[length];
        for (var i = 0; i < length; i++)
        {
            chars[i] = validCharacters[Random.Shared.Next(validCharacters.Length)];
        }
        return new string(chars);
    }
}