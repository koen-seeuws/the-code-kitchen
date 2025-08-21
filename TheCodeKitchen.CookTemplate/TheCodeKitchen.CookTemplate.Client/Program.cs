using TheCodeKitchen.CookTemplate.Client;

const string apiUrl = "https://ca-tck-cook-api.proudbeach-fbb36fdd.westeurope.azurecontainerapps.io/";

var cook = new Cook(apiUrl);

var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true; // prevent immediate termination
    cancellationTokenSource.Cancel();
    Console.WriteLine("Stopping cook...");
};

try
{
    await cook.StartCooking(cancellationTokenSource.Token); // async connect
    Console.WriteLine("Cooking started. Press Ctrl+C to stop.");

    // Keep the app alive until Ctrl+C
    await Task.Delay(-1, cancellationTokenSource.Token);
}
catch (TaskCanceledException)
{
    Console.WriteLine("Cooking stopped.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
