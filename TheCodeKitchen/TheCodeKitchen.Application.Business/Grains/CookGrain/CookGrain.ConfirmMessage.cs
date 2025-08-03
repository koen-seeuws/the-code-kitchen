using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ConfirmMessage(ConfirmMessageRequest request)
    {
        var message = state.State.Messages.FirstOrDefault(m => m.Number == request.Number);
        
        if (message is null)
            return new NotFoundError($"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        
        state.State.Messages.Remove(message);
        
        await state.WriteStateAsync();
        
        return new TheCodeKitchenUnit();
    }
}