using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> DeliverMessage(DeliverMessageToCookRequest request)
    {
        if (!state.RecordExists)
            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");

        var number = state.State.Messages.Count + 1;
        var message = new Message(number, request.From, request.To, request.Content, request.Timestamp);
        state.State.Messages.Add(message);

        await state.WriteStateAsync();
        
        var @event = new MessageReceivedEvent(number, request.From, request.To, request.Content, request.Timestamp);
        await realTimeCookService.SendMessageReceivedEvent(@event);

        return TheCodeKitchenUnit.Value;
    }
}