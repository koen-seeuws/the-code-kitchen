namespace TheCodeKitchen.Cook.Client.Cooks;

public class Koen2 : Cook
{
    public Koen2(string kitchenCode, TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        Username = "Koen 2";
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