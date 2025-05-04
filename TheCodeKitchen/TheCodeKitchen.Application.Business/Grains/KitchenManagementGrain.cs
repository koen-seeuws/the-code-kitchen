using Orleans;
using Orleans.Runtime;
using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Grains;
using TheCodeKitchen.Application.Contracts.Requests;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public class KitchenCodeIndexState
{
    public IDictionary<string, Guid> KitchenCodes { get; set; } = new Dictionary<string, Guid>();
}

public class KitchenManagementGrain(
    [PersistentState("KitchenCodes")] IPersistentState<KitchenCodeIndexState> state
) : Grain, IKitchenManagementGrain
{
    private const int MaxAttempts = 10;

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (!state.RecordExists)
        {
            state.State = new KitchenCodeIndexState();
            await state.WriteStateAsync(cancellationToken);
        }

        await base.OnActivateAsync(cancellationToken);
    }

    public async Task<Result<string>> GenerateUniqueCode(Guid kitchenId, int length, string characters)
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

                if (state.State.KitchenCodes.Keys.Contains(code)) continue;

                state.State.KitchenCodes.Add(code, kitchenId);
                await state.WriteStateAsync();
                return code;
            }

            length++;
        }
    }


    public async Task<Result<JoinKitchenResponse>> JoinKitchen(JoinKitchenRequest request)
    {
        var retrieved = state.State.KitchenCodes.TryGetValue(request.KitchenCode, out var kitchenId);

        if (!retrieved)
            return new NotFoundError($"The kitchen code {request.KitchenCode} does not exist");

        var kitchenGrain = GrainFactory.GetGrain<IKitchenGrain>(kitchenId);
        var result = await kitchenGrain.JoinKitchen(request);

        return result;
    }

    public async Task<Result<TheCodeKitchenUnit>> DeleteKitchenCode(string code)
    {
        var removed = state.State.KitchenCodes.Remove(code);

        if (!removed)
            return new NotFoundError($"The kitchen code {code} does not exist");

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}