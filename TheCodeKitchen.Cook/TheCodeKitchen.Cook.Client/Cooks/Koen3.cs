namespace TheCodeKitchen.Cook.Client.Cooks;

public class Koen3 : Cook
{
    public Koen3(string kitchenCode, TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        Username = "Koen 3";
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