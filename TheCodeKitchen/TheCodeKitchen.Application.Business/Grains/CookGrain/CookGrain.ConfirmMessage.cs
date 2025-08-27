using Microsoft.Extensions.Logging;
using TheCodeKitchen.Application.Contracts.Requests.Cook;

namespace TheCodeKitchen.Application.Business.Grains.CookGrain;

public sealed partial class CookGrain
{
    public async Task<Result<TheCodeKitchenUnit>> ConfirmMessage(ConfirmMessageRequest request)
    {
        if (!state.RecordExists)
        {
            logger.LogWarning("The cook with username {Username} does not exist in kitchen {Kitchen}",
                this.GetPrimaryKeyString(), this.GetPrimaryKey());
            return new NotFoundError(
                $"The cook with username {this.GetPrimaryKeyString()} does not exist in kitchen {this.GetPrimaryKey()}");
        }

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Confirming message with number {MessageNumber}...",
            state.State.Kitchen, state.State.Username, request.Number);

        var message = state.State.Messages.FirstOrDefault(m => m.Number == request.Number);

        if (message is null)
        {
            logger.LogWarning(
                "Kitchen {Kitchen} - Cook {Username}: No message with number {MessageNumber} addressed to this cook",
                state.State.Kitchen, state.State.Username, request.Number);
            return new NotFoundError($"You don't have a message with number {request.Number} addressed to you");
        }

        state.State.Messages.Remove(message);

        await state.WriteStateAsync();

        logger.LogInformation("Kitchen {Kitchen} - Cook {Username}: Message with number {MessageNumber} confirmed",
            state.State.Kitchen, state.State.Username, request.Number);

        return new TheCodeKitchenUnit();
    }
}