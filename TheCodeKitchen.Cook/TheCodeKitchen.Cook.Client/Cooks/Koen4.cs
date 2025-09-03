namespace TheCodeKitchen.Cook.Client.Cooks;

public class Koen4 : Cook
{
    public Koen4(string kitchenCode, TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        Username = "Koen 4";
        Password = "TEST1234";
        KitchenCode = kitchenCode;
        
        OnKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent =>
        {
  
        };
        
        OnTimerElapsedEvent = async timerElapsedEvent =>
        {
            
        };
        
        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            
        };
    }
}