using TheCodeKitchen.Cook.Client;
using TheCodeKitchen.Cook.Client.Cooks;

const string apiUrl = "https://ca-tck-cook-api.proudbeach-fbb36fdd.westeurope.azurecontainerapps.io/";

const string kitchenCode = "YI4N";
const string password = "P@ssw0rd";

const string headChefUsername = "Head Chef Koen";
string[] chefUsernames = ["Koen 1", "Koen 2", "Koen 3"];

var headChefClient = new TheCodeKitchenClient(apiUrl);
var headChef = new HeadChef(chefUsernames, kitchenCode, headChefUsername, password, headChefClient);

var chefs = chefUsernames
    .Select(chefUsername =>
    {
        var chefClient = new TheCodeKitchenClient(apiUrl);
        var chef = new Chef(headChefUsername, kitchenCode, chefUsername, password, chefClient);
        return chef;
    })
    .ToArray();

var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // Prevent immediate termination
    cancellationTokenSource.Cancel();
    Console.WriteLine("\nStopping cooking...");
};

try
{
    Console.WriteLine("Starting cooking...");

    var headChefTask = headChef.StartCooking(cancellationTokenSource.Token);
    var chefTasks = chefs.Select(chef => chef.StartCooking(cancellationTokenSource.Token)).ToArray();
    
    await Task.WhenAll(chefTasks.Append(headChefTask));
    
    Console.WriteLine("\nStarted cooking. Press Ctrl+C to stop.");
    await Task.Delay(-1, cancellationTokenSource.Token); // Keep the app alive until Ctrl+C
}
catch (TaskCanceledException)
{
    Console.WriteLine("\nStopped cooking.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}