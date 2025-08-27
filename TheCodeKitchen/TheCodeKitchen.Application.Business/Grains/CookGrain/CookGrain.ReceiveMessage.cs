using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Events.Cook;
using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ReceiveMessage(ReceiveMessageRequest request)
    {
        if (!state.RecordExists)
        {
            logger.LogWarning("The cook with username {Username} does not exist in kitchen {Kitchen}",
                this.GetPrimaryKeyString(), this.GetPrimaryKey());

            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        }

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Receiving message from {From}...",
            state.State.Kitchen, state.State.Username, request.From);

        var number = state.State.Messages.Count + 1;
        var message = new Message(number, request.From, request.To, request.Content, request.Timestamp);
        state.State.Messages.Add(message);

        await state.WriteStateAsync();
        
        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Message from {From} received with number {MessageNumber}",
            state.State.Kitchen, state.State.Username, request.From, number);

        var @event = new MessageReceivedEvent(number, request.From, request.To, request.Content, request.Timestamp);
        await realTimeCookService.SendMessageReceivedEvent(@event);

        return TheCodeKitchenUnit.Value;
    }
}