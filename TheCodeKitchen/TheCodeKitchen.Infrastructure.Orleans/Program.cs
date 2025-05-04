using TheCodeKitchen.Application.Business;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

builder.UseOrleans(silo =>
{
    if (builder.Environment.IsDevelopment())
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorageAsDefault();
        silo.ConfigureLogging(logging => { logging.AddConsole(); });
    }
    
    silo.UseDashboard();
});

var host = builder.Build();
host.Run();