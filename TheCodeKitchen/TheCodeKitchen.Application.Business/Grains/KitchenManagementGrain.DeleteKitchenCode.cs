using TheCodeKitchen.Application.Contracts.Errors;
using TheCodeKitchen.Application.Contracts.Results;

namespace TheCodeKitchen.Application.Business.Grains;

public partial class KitchenManagementGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeleteKitchenCode(string code)
    {
        var removed = state.State.KitchenCodes.Remove(code);

        if (!removed)
            return new NotFoundError($"The kitchen code {code} does not exist");

        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}