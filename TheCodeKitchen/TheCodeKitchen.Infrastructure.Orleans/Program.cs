using TheCodeKitchen.Application.Business;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddOrleans(silo =>
{
    if (builder.Environment.IsDevelopment())
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorageAsDefault();
        silo.ConfigureLogging(logging => { logging.AddConsole(); });
    }
});

var host = builder.Build();
host.Run();