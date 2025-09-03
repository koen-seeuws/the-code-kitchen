using System.Text.Json;
using TheCodeKitchen.Cook.Client.Custom;
using TheCodeKitchen.Cook.Contracts.Reponses.CookBook;
using TheCodeKitchen.Cook.Contracts.Reponses.Pantry;

namespace TheCodeKitchen.Cook.Client.Cooks;

public class Chef : Cook
{
    private readonly TheCodeKitchenClient _theCodeKitchenClient;
    private ICollection<Message> _messages = new List<Message>();
    private ICollection<GetIngredientResponse> _ingredients = new List<GetIngredientResponse>();
    private ICollection<GetRecipeResponse> _recipes = new List<GetRecipeResponse>();
    private readonly string _headChef;

    public Chef(string headChef, string kitchenCode, string username, string password,
        TheCodeKitchenClient theCodeKitchenClient) : base(theCodeKitchenClient)
    {
        _headChef = headChef;
        _theCodeKitchenClient = theCodeKitchenClient;
        Username = username;
        Password = password;
        KitchenCode = kitchenCode;

        OnKitchenOrderCreatedEvent = async kitchenOrderCreatedEvent => { };

        OnTimerElapsedEvent = async timerElapsedEvent => { };

        OnMessageReceivedEvent = async messageReceivedEvent =>
        {
            var message = new Message(
                messageReceivedEvent.Number,
                messageReceivedEvent.From,
                messageReceivedEvent.To,
                JsonSerializer.Deserialize<MessageContent>(messageReceivedEvent.Content)!
            );
            Console.WriteLine($"{Username} - Message Received - {JsonSerializer.Serialize(message)}");
            _messages.Add(message);
        };
    }

    public override async Task StartCooking(CancellationToken cancellationToken = default)
    {
        await base.StartCooking(cancellationToken);

        _ingredients = await _theCodeKitchenClient.PantryInventory(cancellationToken);
        _recipes = await _theCodeKitchenClient.ReadRecipes(cancellationToken);

        var messages = await _theCodeKitchenClient.ReadMessages(cancellationToken);
        _messages = messages
            .Select(m => new Message(
                    m.Number,
                    m.From,
                    m.To,
                    JsonSerializer.Deserialize<MessageContent>(m.Content)!
                )
            )
            .ToList();
    }
}