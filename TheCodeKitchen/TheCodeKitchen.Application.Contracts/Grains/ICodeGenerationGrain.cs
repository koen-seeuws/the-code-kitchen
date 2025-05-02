using Orleans;

namespace TheCodeKitchen.Application.Contracts.Grains;

public interface ICodeGenerationGrain : IGrainWithGuidKey
{
    Task<string> GenerateUniqueCode(int length = 4, string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
}