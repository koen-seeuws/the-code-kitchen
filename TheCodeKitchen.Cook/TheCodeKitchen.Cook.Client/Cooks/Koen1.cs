namespace TheCodeKitchen.Cook.Client.Cooks;

public class Koen1 : Cook
{
    public Koen1(string kitchenCode, TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        Username = "Koen 1";
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