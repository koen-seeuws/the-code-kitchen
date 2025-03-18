namespace TheCodeKitchen.Application.Contracts.Interfaces.Common;

public interface ICodeGenerator
{
    string GenerateCode(int length = 4, string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
}