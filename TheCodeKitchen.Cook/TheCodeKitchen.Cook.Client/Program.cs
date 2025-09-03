using TheCodeKitchen.Cook.Client;
using TheCodeKitchen.Cook.Client.Cooks;

const string apiUrl = "https://ca-tck-cook-api.proudbeach-fbb36fdd.westeurope.azurecontainerapps.io/";

const string kitchenCode = "6SYE"; 

var koen1Client = new TheCodeKitchenClient(apiUrl);
var koen1 = new Koen1(kitchenCode, koen1Client);

var koen2Client = new TheCodeKitchenClient(apiUrl);
var koen2 = new Koen2(kitchenCode, koen2Client);

var koen3Client = new TheCodeKitchenClient(apiUrl);
var koen3 = new Koen3(kitchenCode, koen3Client);

var koen4Client = new TheCodeKitchenClient(apiUrl);
var koen4 = new Koen4(kitchenCode, koen4Client);

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

    await koen1.StartCooking();
    await koen2.StartCooking();
    await koen3.StartCooking();
    await koen4.StartCooking();
    
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
