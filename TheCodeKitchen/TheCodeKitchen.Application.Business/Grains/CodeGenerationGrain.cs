using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Grains;

namespace TheCodeKitchen.Application.Business.Grains;

public class CodeGenerationState
{
    public ICollection<string> Codes { get; } = new List<string>();
}

public class CodeGenerationGrain(
    [PersistentState("Codes")] IPersistentState<CodeGenerationState> state
) : Grain, ICodeGenerationGrain
{
    private const int MaxAttempts = 100;

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new CodeGenerationState();
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public async Task<string> GenerateUniqueCode(
        int length = 4,
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    )
    {
        var validCharacters = characters.ToCharArray();

        while (true)
        {
            for (var attempt = 0; attempt < MaxAttempts; attempt++)
            {
                var chars = new char[length];
                for (var i = 0; i < length; i++)
                {
                    chars[i] = validCharacters[Random.Shared.Next(validCharacters.Length)];
                }

                var code = new string(chars);

                if (state.State.Codes.Contains(code)) continue;

                state.State.Codes.Add(code);
                await state.WriteStateAsync();
                return code;
            }

            length++;
        }
    }
}