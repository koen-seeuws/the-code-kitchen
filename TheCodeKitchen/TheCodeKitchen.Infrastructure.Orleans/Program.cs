using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Application.Contracts.Contants;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

builder.UseOrleans(silo =>
{
    if (builder.Environment.IsDevelopment())
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorageAsDefault();
        silo.AddStreaming()
            .AddMemoryStreams(TheCodeKitchenStreams.Default);
        
        silo.ConfigureLogging(logging => { logging.AddConsole(); });
    }
    
    silo.UseDashboard();
});

var host = builder.Build();
host.Run();