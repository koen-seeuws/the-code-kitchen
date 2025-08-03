using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeliverMessage(DeliverMessageToCookRequest toCookRequest)
    {
        if (!state.RecordExists)
            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");

        var number = state.State.Messages.Count + 1;
        var message = new Message(number, toCookRequest.From, toCookRequest.To, toCookRequest.Content, toCookRequest.Timestamp);
        state.State.Messages.Add(message);
        
        await state.WriteStateAsync();

        return TheCodeKitchenUnit.Value;
    }
}