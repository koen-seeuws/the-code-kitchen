using TheCodeKitchen.CookTemplate.Client;

const string apiUrl = "https://ca-tck-cook-api.proudbeach-fbb36fdd.westeurope.azurecontainerapps.io/";

var cook = new Cook(apiUrl);

var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // prevent immediate termination
    cancellationTokenSource.Cancel();
    Console.WriteLine("\nStopping cooking...");
};

try
{
    Console.WriteLine("Starting cooking...");
    await cook.StartCooking(cancellationTokenSource.Token); // async connect
    Console.WriteLine("\nStarted cooking. Press Ctrl+C to stop.");

    // Keep the app alive until Ctrl+C
    await Task.Delay(-1, cancellationTokenSource.Token);
}
catch (TaskCanceledException)
{
    Console.WriteLine("\nStopped cooking.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
