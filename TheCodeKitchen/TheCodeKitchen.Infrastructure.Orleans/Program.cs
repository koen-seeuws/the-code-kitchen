using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Application.Business.Contants;
using Stream = TheCodeKitchen.Application.Business.Contants.Stream;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

builder.UseOrleans(silo =>
{
    if (builder.Environment.IsDevelopment())
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorageAsDefault();
        silo.AddStreaming()
            .AddMemoryStreams(Stream.Default);
        
        silo.ConfigureLogging(logging => { logging.AddConsole(); });
    }
    
    silo.UseDashboard();
});

var host = builder.Build();
host.Run();