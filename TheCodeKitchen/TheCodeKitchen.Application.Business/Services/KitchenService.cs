using TheCodeKitchen.Application.Contracts.Interfaces.Common;

namespace TheCodeKitchen.Application.Business.Services;

public interface IKitchenService
{
    Task<string> GenerateUniqueCode(short attemptsPerLength = 10, CancellationToken cancellationToken = default);
}

public class KitchenService(
    ICodeGenerator codeGenerator,
    IKitchenRepository kitchenRepository
) : IKitchenService
{
    public async Task<string> GenerateUniqueCode(short attemptsPerLength = 10, CancellationToken cancellationToken = default)
    {
        var length = 4;
        var attempts = 0;

        while (true)
        {
            var code = codeGenerator.GenerateCode(length);
            var exists = await kitchenRepository.CodeExists(code, cancellationToken);
            if (!exists)
                return code;

            attempts++;
            if (attempts < attemptsPerLength) continue;
            attempts = 0;
            length++;
        }
    }
}