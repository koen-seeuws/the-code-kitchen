namespace TheCodeKitchen.Application.Business.Grains.GameGrain;

public partial class GameGrain : IRemindable
{
    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        switch (reminderName)
        {
            case nameof(ResumeGame):
                await ResumeGame();
                break;
            default:
                throw new NotImplementedException();
        }
    }
}